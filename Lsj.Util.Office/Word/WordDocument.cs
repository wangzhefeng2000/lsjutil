﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop;
using Lsj.Util;
using System.Drawing;

namespace Lsj.Util.Office.Word
{
    public class WordDocument :DisposableClass, IDisposable
    {
        Application app;
        Document doc;
        public WordDocument()
        {
            app = new Application();
            doc = app.Documents.Add();
            app.Visible = true;
            this.Sections = new Sections(doc);
            this.TablesOfContents = new TablesOfContents(doc);
        }
        protected override void CleanUpUnmanagedResources()
        {
            app.Quit(true);
            base.CleanUpUnmanagedResources();
        }


        public Sections Sections
        {
            get;
        }
        public TablesOfContents TablesOfContents
        {
            get;
        }


        public void SetDocPaper(WdPaperSize size)
        {
            doc.PageSetup.PaperSize = size;
        }
        public void SetDocMargin(float? left, float? right, float? top, float? bottom)
        {
            if (left != null)
            {
                doc.PageSetup.LeftMargin = left.Value;
            }
            if (right != null)
            {
                doc.PageSetup.RightMargin = right.Value;
            }
            if (top != null)
            {
                doc.PageSetup.TopMargin = top.Value;
            }
            if (bottom != null)
            {
                doc.PageSetup.BottomMargin = bottom.Value;
            }

        }




        public void SaveAs(string filename)
        {
            doc.SaveAs2(filename);
        }
        public void Close()
        {
            doc.Close(true);
        }


        public void AddPageNumberAtFooterForFirstSection()
        {
            this.Sections[0].AddPageNumberAtFooter();
        }



        public void AppendBlankLine(int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.AppendLine();
            }
        }
        public void AppendLine(string str = null) => Append(str + "\n");
        public void Append(string str)
        {
            app.Options.ReplaceSelection = false;
            GoToEnd();
            var selection = app.Selection;
            selection.TypeText(str);
        }
        public void AppendPage()
        {
            app.Options.ReplaceSelection = false;
            GoToEnd();
            var selection = app.Selection;
            selection.InsertBreak(WdBreakType.wdPageBreak);
            selection.Delete(WdUnits.wdCharacter, -1);
        }
        public void AppendSection()
        {
            app.Options.ReplaceSelection = false;
            GoToEnd();
            var selection = app.Selection;
            selection.InsertBreak(WdBreakType.wdSectionBreakNextPage);
        }
        public void AppendTableOfContents()
        {
            GoToEnd();
            var selection = app.Selection;
            doc.Fields.Add(selection.Range, WdFieldType.wdFieldTOC, @"", true);
        }

        public void SetAppendStyle(int? size = null, string fontname = null, eParagraphAlignment? alignment = null, Color? color = null, bool? bold = null, bool? italic = null, eUnderline? underline = null, eBuiltinStyle? style = null)
        {
            GoToEnd();
            SetSelectionStyle(size, fontname, alignment, color, bold, italic, underline, style);
        }


        public void SetSelectionStyle(int? size = null, string fontname = null, eParagraphAlignment? alignment = null, Color? color = null, bool? bold = null, bool? italic = null, eUnderline? underline = null, eBuiltinStyle? style = null)
        {
            var selection = app.Selection;

            if (size != null)
            {
                selection.Font.Size = size.Value;
            }
            if (fontname != null)
            {
                selection.Font.Name = fontname;
            }
            if (alignment != null)
            {
                selection.ParagraphFormat.Alignment = (WdParagraphAlignment)alignment.Value;
            }
            if (color != null)
            {
                selection.Font.Color = (WdColor)(color.Value.R + color.Value.G * 0x100 + color.Value.B * 0x10000);
            }
            if (bold != null)
            {
                if (bold.Value)
                {
                    selection.Font.Bold = 1;
                }
                else
                {
                    selection.Font.Bold = 0;
                }
            }
            if (italic != null)
            {
                if (italic.Value)
                {
                    selection.Font.Italic = 1;
                }
                else
                {
                    selection.Font.Italic = 0;
                }
            }
            if (underline != null)
            {
                selection.Font.Underline = (WdUnderline)underline;
            }
            if (style != null)
            {
                selection.set_Style(style);
            }
        }

        public void UpdateAllTableOfContents()
        {
            foreach (var a in this.TablesOfContents)
            {
                a.Update();
            }
        }



        public float InchesToPoints(double inch) => app.InchesToPoints((float)inch);
        public float MillimetersToPoints(double mm) => app.MillimetersToPoints((float)mm);

        public void GoToEnd()
        {
            var selection = app.Selection;
            selection.GoTo(WdGoToItem.wdGoToLine, WdGoToDirection.wdGoToLast);
            while (selection.MoveRight(WdUnits.wdCharacter) == 1)
            {
            }

        }

    }
}