using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Word;
using sys = System;



namespace Bobs_PO_Generator
{

  public class CreateDoc
  {
    public static void GenerateDoc()
    {
      sys.Console.WriteLine("Demo - Creating Word documents from templates");

      var app = new Application();
      try
      {
        //This code creates a document based on the specified template.
        var doc = app.Documents.Add(
            Path.GetFullPath(@"Data\PO.dotx"), Visible: false);

        doc.Activate();

        //do this for each keyword you want to replace.
        var sel = app.Selection;
        sel.Find.Text = "[Company]";
        sel.Find.Replacement.Text = "amiralles";
        sel.Find.Wrap = WdFindWrap.wdFindContinue;
        sel.Find.Forward = true;
        sel.Find.Format = false;
        sel.Find.MatchCase = false;
        sel.Find.MatchWholeWord = false;
        sel.Find.Execute(Replace: WdReplace.wdReplaceAll);
        //************************************************

        doc.SaveAs(Path.GetFullPath(@"Data\PO.docx"));
        doc.Close();
      }
      finally
      {
        //SUPER IMPORTANT!
        //If you don't do this, each time you run the code 
        //the winword.exe process will keep running on background (for ever!),
        //at 10MB a piece, you may end up with a huge memory leak.
        app.Quit();
        sys.Runtime.InteropServices.Marshal.FinalReleaseComObject(app);
        //
      }
      sys.Console.WriteLine("Press [Enter] to exit");
      sys.Console.ReadLine();
    }

  }

}

  

