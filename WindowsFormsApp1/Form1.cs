using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateNewTab(); // Khi mở chương trình sẽ có sẵn 1 tab mới
        }

        // 🧱 Lấy RichTextBox hiện tại trong tab đang chọn
        private RichTextBox GetCurrentRichTextBox()
        {
            if (tabControlMain.SelectedTab != null && tabControlMain.SelectedTab.Controls.Count > 0)
                return tabControlMain.SelectedTab.Controls[0] as RichTextBox;
            return null;
        }

        // 🧱 Tạo tab mới
        private void CreateNewTab(string title = "Untitled")
        {
            TabPage newTab = new TabPage(title);
            RichTextBox rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;
            rtb.Font = new Font("Consolas", 11);
            newTab.Controls.Add(rtb);
            tabControlMain.TabPages.Add(newTab);
            tabControlMain.SelectedTab = newTab;
        }

        // 🆕 File → New
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewTab();
        }

        // 📂 File → Open
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialogMain.FileName;
                string fileText = File.ReadAllText(filePath, Encoding.UTF8);
                CreateNewTab(Path.GetFileName(filePath));

                RichTextBox rtb = GetCurrentRichTextBox();
                rtb.Text = fileText;
                rtb.Tag = filePath; // Lưu đường dẫn để Save nhanh
            }
        }

        // 💾 File → Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            string filePath = rtb.Tag as string;

            if (string.IsNullOrEmpty(filePath))
            {
                saveAsToolStripMenuItem_Click(sender, e); // Nếu chưa lưu, mở Save As
                return;
            }

            File.WriteAllText(filePath, rtb.Text, Encoding.UTF8);
            tabControlMain.SelectedTab.Text = Path.GetFileName(filePath);
        }

        // 💾 File → Save As
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            if (saveFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialogMain.FileName;
                File.WriteAllText(filePath, rtb.Text, Encoding.UTF8);

                rtb.Tag = filePath;
                tabControlMain.SelectedTab.Text = Path.GetFileName(filePath);
            }
        }

        // ❌ File → Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        // --- SỬ DỤNG GetCurrentRichTextBox() THAY CHO richTextBox1 ---

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return; // Kiểm tra nếu không có tab nào đang mở

            rtb.Focus();
            // Kiểm tra xem có văn bản nào được chọn không
            if (rtb.SelectionLength > 0)
            {
                rtb.Cut();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            rtb.Focus();
            // Kiểm tra xem có văn bản nào được chọn không
            if (rtb.SelectionLength > 0)
            {
                rtb.Copy();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            rtb.Focus();
            // Kiểm tra xem clipboard có chứa văn bản không
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                rtb.Paste();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            rtb.Focus();
            rtb.SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            rtb.Focus();
            rtb.SelectAll();
        }

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            // Giới hạn phóng to để không bị quá lớn
            if (rtb.ZoomFactor < 5.0f)
            {
                rtb.ZoomFactor += 0.2f; // Tăng kích thước zoom lên 0.2
            }
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            if (rtb.CanUndo)
            {
                rtb.Undo();
            }
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            if (rtb.CanRedo)
            {
                rtb.Redo();
            }
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            // Giới hạn thu nhỏ để không bị quá bé
            if (rtb.ZoomFactor > 0.5f)
            {
                rtb.ZoomFactor -= 0.2f; // Giảm kích thước zoom đi 0.2
            }
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("zalo 0393175190 ");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        { }
      
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem TabControl có đang chứa bất kỳ Tab nào không
            // (Giả sử TabControl của bạn có tên là tabControl1)
            if (tabControlMain.TabPages.Count > 0)
            {
                // Lấy TabPage đang được chọn (Tab người dùng đang xem)
                TabPage tabToRemove = tabControlMain.SelectedTab;

                // Xóa TabPage đó khỏi TabControl
                tabControlMain.TabPages.Remove(tabToRemove);

                // Tùy chọn: Xử lý nếu đó là Tab cuối cùng
                if (tabControlMain.TabPages.Count == 0)
                {
                    // Ví dụ: Tạo lại một Tab mới trống (thường là hành vi mong muốn cho Notepad)
                    TabPage newTabPage = new TabPage("Untitled");
                    RichTextBox newRichTextBox = new RichTextBox { Dock = DockStyle.Fill };
                    newTabPage.Controls.Add(newRichTextBox);
                    tabControlMain.TabPages.Add(newTabPage);
                    tabControlMain.SelectedTab = newTabPage;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // 1. Đếm số lượng tab hiện có để đặt tên cho tab mới
            int newTabIndex = tabControlMain.TabPages.Count + 1;

            // 2. Tạo một TabPage mới
            TabPage newTabPage = new TabPage("Untitled " + newTabIndex);

            // 3. Tạo RichTextBox mới
            RichTextBox newRichTextBox = new RichTextBox();

            // 4. Cấu hình RichTextBox để lấp đầy toàn bộ TabPage
            newRichTextBox.Dock = DockStyle.Fill;
            newRichTextBox.Name = "RichTB_" + newTabIndex; // Đặt tên riêng để dễ quản lý sau này

            // 5. Thêm RichTextBox vào TabPage
            newTabPage.Controls.Add(newRichTextBox);

            // 6. Thêm TabPage mới vào TabControl
            tabControlMain.TabPages.Add(newTabPage);

            // 7. Chuyển ngay sang Tab mới tạo để người dùng bắt đầu làm việc
            tabControlMain.SelectedTab = newTabPage;
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            CreateNewTab();
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialogMain.FileName;
                string fileText = File.ReadAllText(filePath, Encoding.UTF8);
                CreateNewTab(Path.GetFileName(filePath));

                RichTextBox rtb = GetCurrentRichTextBox();
                rtb.Text = fileText;
                rtb.Tag = filePath; // Lưu đường dẫn để Save nhanh
            }
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            string filePath = rtb.Tag as string;

            if (string.IsNullOrEmpty(filePath))
            {
                saveAsToolStripMenuItem_Click(sender, e); // Nếu chưa lưu, mở Save As
                return;
            }

            File.WriteAllText(filePath, rtb.Text, Encoding.UTF8);
            tabControlMain.SelectedTab.Text = Path.GetFileName(filePath);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            rtb.Focus();
            rtb.SelectedText = "";
        }

        private void toolStripButtonCut_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return; // Kiểm tra nếu không có tab nào đang mở

            rtb.Focus();
            // Kiểm tra xem có văn bản nào được chọn không
            if (rtb.SelectionLength > 0)
            {
                rtb.Cut();
            }
        }
        private int lastSearchIndex = 0;
        private string lastSearchText = "";
        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            // Nếu người dùng tìm lần đầu hoặc muốn tìm từ khóa mới
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Nhập từ cần tìm:",
                "Tìm kiếm văn bản",
                lastSearchText
            );

            if (string.IsNullOrEmpty(input)) return;

            // Nếu thay đổi từ khóa, bắt đầu lại từ đầu
            if (input != lastSearchText)
            {
                lastSearchIndex = 0;
                lastSearchText = input;
            }

            int index = rtb.Find(input, lastSearchIndex, RichTextBoxFinds.None);

            if (index != -1)
            {
                // Tô sáng đoạn văn bản tìm thấy
                rtb.Select(index, input.Length);
                rtb.ScrollToCaret();
                rtb.Focus();

                // Lưu vị trí để tìm tiếp ở lần sau
                lastSearchIndex = index + input.Length;
            }
            else
            {
                MessageBox.Show("Không tìm thấy chuỗi \"" + input + "\" nữa.", "Kết quả tìm kiếm");
                lastSearchIndex = 0; // reset để tìm lại từ đầu
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox(); if (rtb == null) return;
            rtb.Focus();
            rtb.SelectAll();
            rtb.Focus();
            lastSearchText = rtb.Text;
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Nhập từ cần tìm:",
                "Tìm kiếm văn bản",
                lastSearchText
            );

            if (string.IsNullOrEmpty(input)) return;

            if (input != lastSearchText)
            {
                lastSearchIndex = 0;
                lastSearchText = input;
            }

            int index = rtb.Find(input, lastSearchIndex, RichTextBoxFinds.None);

            if (index != -1)
            {
                rtb.Select(index, input.Length);
                rtb.ScrollToCaret();
                rtb.Focus();

                lastSearchIndex = index + input.Length;
            }
            else
            {
                MessageBox.Show("Không tìm thấy chuỗi \"" + input + "\" nữa.", "Kết quả tìm kiếm");
                lastSearchIndex = 0;
            }
        }
        // Add this field (ensure highlight color = yellow)
        private readonly Color toggleHighlightColor = Color.Yellow;

        // Toggle highlight for current selection (use background color)
        private void ToggleHighlightSelection(RichTextBox rtb, Color highlightColor)
        {
            if (rtb.SelectionLength == 0) return;

            int selStart = rtb.SelectionStart;
            int selLength = rtb.SelectionLength;

            // If entire selection already has the highlight color, remove it; otherwise apply it.
            // Use SelectionBackColor so the highlight is a yellow background.
            Color currentBackColor = rtb.SelectionBackColor;
            Color newColor = currentBackColor == highlightColor ? rtb.BackColor : highlightColor;

            rtb.SelectionBackColor = newColor;

            // Restore original selection
            rtb.Select(selStart, selLength);
        }
        // Add these fields
        private string lastHighlightedTerm = null;
        private bool isTermHighlighted = false;

        // Modified HighlightAll (restore selection without forcing backcolor)
        private void HighlightAll(RichTextBox rtb, string searchText, Color highlightColor, bool matchCase)
        {
            if (string.IsNullOrEmpty(searchText)) return;

            int selStart = rtb.SelectionStart;
            int selLength = rtb.SelectionLength;

            RichTextBoxFinds options = matchCase ? RichTextBoxFinds.MatchCase : RichTextBoxFinds.None;
            int start = 0;

            while (true)
            {
                int index = rtb.Find(searchText, start, options);
                if (index == -1) break;
                rtb.Select(index, searchText.Length);
                rtb.SelectionBackColor = highlightColor;
                start = index + searchText.Length;
            }

            // Restore original selection (do not change its backcolor)
            rtb.Select(selStart, selLength);
        }

        // New: clear highlights for a specific term
        private void ClearHighlightsForTerm(RichTextBox rtb, string searchText, bool matchCase = false)
        {
            if (string.IsNullOrEmpty(searchText)) return;

            int selStart = rtb.SelectionStart;
            int selLength = rtb.SelectionLength;

            RichTextBoxFinds options = matchCase ? RichTextBoxFinds.MatchCase : RichTextBoxFinds.None;
            int start = 0;

            while (true)
            {
                int index = rtb.Find(searchText, start, options);
                if (index == -1) break;
                rtb.Select(index, searchText.Length);
                rtb.SelectionBackColor = rtb.BackColor;
                start = index + searchText.Length;
            }

            rtb.Select(selStart, selLength);
        }

        // Updated handler: toggle highlights when no selection
        private void toolStripButtonHighlight_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox(); if (rtb == null) return;

            if (rtb.SelectionLength > 0)
            {
                // Toggle highlight on selected text
                ToggleHighlightSelection(rtb, toggleHighlightColor);
                return;
            }

            // No selection -> ask for text to highlight across document
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Nhập từ cần tìm:",
                "Tô màu tất cả",
                ""
            );
            if (string.IsNullOrEmpty(input)) return;
            HighlightAll(rtb, input, toggleHighlightColor, matchCase: false);

            // If the same term is already highlighted -> remove highlights
            if (isTermHighlighted && string.Equals(lastHighlightedTerm, input, StringComparison.Ordinal))
            {
                ClearHighlightsForTerm(rtb, input, matchCase: false);
                lastHighlightedTerm = null;
                isTermHighlighted = false;
                return;
            }

            // If another term was highlighted, clear it first
            if (isTermHighlighted && !string.IsNullOrEmpty(lastHighlightedTerm))
            {
                ClearHighlightsForTerm(rtb, lastHighlightedTerm, matchCase: false);
                lastHighlightedTerm = null;
                isTermHighlighted = false;
            }

            // Highlight new term and remember it
            HighlightAll(rtb, input, toggleHighlightColor, matchCase: false);
            lastHighlightedTerm = input;
            isTermHighlighted = true;
        }
        // Add this method to your Form1 class
        private void ClearHighlights(RichTextBox rtb)
        {
            int selStart = rtb.SelectionStart;
            int selLength = rtb.SelectionLength;

            rtb.SelectAll();
            rtb.SelectionBackColor = rtb.BackColor;

            // Restore original selection
            rtb.Select(selStart, selLength);
        }
        // Clear highlights handler (if you want separate button/menu)
        private void toolStripButtonClearHighlights_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;
            ClearHighlights(rtb);
        }
        // Add this method to your Form1 class
        private void SyntaxHighlightKeywords(RichTextBox rtb, string[] keywords, Color color, bool matchCase)
        {
            int selStart = rtb.SelectionStart;
            int selLength = rtb.SelectionLength;

            // Save the original color
            Color defaultColor = rtb.ForeColor;

            // Remove previous coloring
            rtb.SelectAll();
            rtb.SelectionColor = defaultColor;

            RichTextBoxFinds options = matchCase ? RichTextBoxFinds.MatchCase : RichTextBoxFinds.None;

            foreach (string keyword in keywords)
            {
                int start = 0;
                while (true)
                {
                    int index = rtb.Find(keyword, start, options);
                    if (index == -1) break;
                    rtb.Select(index, keyword.Length);
                    rtb.SelectionColor = color;
                    start = index + keyword.Length;
                }
            }

            // Restore original selection
            rtb.Select(selStart, selLength);
            rtb.SelectionColor = defaultColor;
        }
        private void toolStripButtonSyntaxHighlight_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            string[] csharpKeywords = new[]
            {
                "using", "namespace", "class", "public", "private", "protected", "static",
                "void", "int", "string", "bool", "return", "new", "if", "else", "for", "foreach",
                "while", "switch", "case", "break", "continue", "try", "catch", "finally"
            };

            SyntaxHighlightKeywords(rtb, csharpKeywords, Color.Blue, matchCase: false);
        }
        
        // Add these fields (place inside Form1 class)
private int pluginTabSize = 4;

        // Add these methods and handlers (place inside Form1 class)
        private void CountLinesInCurrent()
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;
            int lines = rtb.Lines?.Length ?? 0;
            MessageBox.Show($"Current document has {lines} line{(lines == 1 ? "" : "s")}.", "Line Count");
        }

        private enum WhitespaceMode { TabsToSpaces = 1, SpacesToTabs = 2, TrimTrailing = 3 }

        private void ConvertWhitespaceInCurrent(WhitespaceMode mode)
        {
            RichTextBox rtb = GetCurrentRichTextBox();
            if (rtb == null) return;

            int selStart = rtb.SelectionStart;
            int selLength = rtb.SelectionLength;

            string text = rtb.Text;
            string newText = text;
            string spaces = new string(' ', pluginTabSize);

            switch (mode)
            {
                case WhitespaceMode.TabsToSpaces:
                    newText = text.Replace("\t", spaces);
                    break;
                case WhitespaceMode.SpacesToTabs:
                    // simple replacement of groups of N spaces -> tab
                    newText = text.Replace(spaces, "\t");
                    break;
                case WhitespaceMode.TrimTrailing:
                    // remove trailing spaces/tabs on each line
                    newText = Regex.Replace(text, "[ \\t]+(?=\\r?$)", "", RegexOptions.Multiline);
                    break;
            }

            if (newText != text)
            {
                rtb.Text = newText;
                // restore selection safely
                rtb.SelectionStart = Math.Min(selStart, rtb.Text.Length);
                rtb.SelectionLength = Math.Min(selLength, Math.Max(0, rtb.Text.Length - rtb.SelectionStart));
            }

            MessageBox.Show("Plugin: whitespace conversion completed.", "Plugin");
        }

        // Simple handlers you can wire from Designer (or call directly)
        private void pluginCountLinesToolStripMenuItem_Click(object sender, EventArgs e) => CountLinesInCurrent();

        private void pluginConvertWhitespaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Choose option:\n1 - Tabs → Spaces\n2 - Spaces → Tabs\n3 - Trim trailing whitespace\n(Default 1)",
                "Whitespace Conversion",
                "1"
            );
            if (string.IsNullOrEmpty(input)) return;
            if (!int.TryParse(input, out int choice)) return;
            if (choice < 1 || choice > 3) return;
            ConvertWhitespaceInCurrent((WhitespaceMode)choice);
        }

        private void toolStripButtonPaste_Click(object sender, EventArgs e)
        {

        }
    }
    
}
