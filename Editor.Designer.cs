partial class Editor
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.splitContainer1 = new System.Windows.Forms.SplitContainer();
        this.lineNumbers = new System.Windows.Forms.RichTextBox();
        this.textEditor = new System.Windows.Forms.RichTextBox();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
        this.splitContainer1.Panel1.SuspendLayout();
        this.splitContainer1.Panel2.SuspendLayout();
        this.splitContainer1.SuspendLayout();
        this.SuspendLayout();
        // 
        // splitContainer1
        // 
        this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainer1.IsSplitterFixed = true;
        this.splitContainer1.Location = new System.Drawing.Point(0, 0);
        this.splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        this.splitContainer1.Panel1.Controls.Add(this.lineNumbers);
        this.splitContainer1.Panel1MinSize = 20;
        // 
        // splitContainer1.Panel2
        // 
        this.splitContainer1.Panel2.Controls.Add(this.textEditor);
        this.splitContainer1.Size = new System.Drawing.Size(646, 592);
        this.splitContainer1.SplitterDistance = 25;
        this.splitContainer1.TabIndex = 0;
        // 
        // lineNumbers
        // 
        this.lineNumbers.BackColor = System.Drawing.SystemColors.ControlLight;
        this.lineNumbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.lineNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
        this.lineNumbers.Enabled = false;
        this.lineNumbers.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lineNumbers.Location = new System.Drawing.Point(0, 0);
        this.lineNumbers.Name = "lineNumbers";
        this.lineNumbers.ReadOnly = true;
        this.lineNumbers.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
        this.lineNumbers.Size = new System.Drawing.Size(25, 592);
        this.lineNumbers.TabIndex = 0;
        this.lineNumbers.Text = "1\n2\n3\n4\n5";
        this.lineNumbers.WordWrap = false;
        // 
        // textEditor
        // 
        this.textEditor.AcceptsTab = true;
        this.textEditor.AutoWordSelection = true;
        this.textEditor.Dock = System.Windows.Forms.DockStyle.Fill;
        this.textEditor.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.textEditor.Location = new System.Drawing.Point(0, 0);
        this.textEditor.Name = "textEditor";
        this.textEditor.Size = new System.Drawing.Size(617, 592);
        this.textEditor.TabIndex = 1;
        this.textEditor.Text = "";
        this.textEditor.WordWrap = false;
        this.textEditor.VScroll += new System.EventHandler(this.textEditor_VScroll);
        this.textEditor.CursorChanged += new System.EventHandler(this.textEditor_CursorChanged);
        this.textEditor.TextChanged += new System.EventHandler(this.textEditor_TextChanged);
        // 
        // Editor
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.splitContainer1);
        this.Name = "Editor";
        this.Size = new System.Drawing.Size(646, 592);
        this.splitContainer1.Panel1.ResumeLayout(false);
        this.splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
        this.splitContainer1.ResumeLayout(false);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox lineNumbers;
    private System.Windows.Forms.RichTextBox textEditor;
}
