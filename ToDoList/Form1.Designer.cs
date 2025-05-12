namespace ToDoList
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            fPanel = new FlowLayoutPanel();
            taskTextBox = new TextBox();
            addTaskButton = new Button();
            SuspendLayout();
            // 
            // fPanel
            // 
            fPanel.AutoScroll = true;
            fPanel.Location = new Point(12, 108);
            fPanel.Name = "fPanel";
            fPanel.Size = new Size(505, 330);
            fPanel.TabIndex = 0;
            // 
            // taskTextBox
            // 
            taskTextBox.Location = new Point(12, 48);
            taskTextBox.Name = "taskTextBox";
            taskTextBox.Size = new Size(323, 27);
            taskTextBox.TabIndex = 1;
            // 
            // addTaskButton
            // 
            addTaskButton.Location = new Point(357, 48);
            addTaskButton.Name = "addTaskButton";
            addTaskButton.Size = new Size(160, 27);
            addTaskButton.TabIndex = 2;
            addTaskButton.Text = "Add Task";
            addTaskButton.UseVisualStyleBackColor = true;
            addTaskButton.Click += addTaskButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(529, 450);
            Controls.Add(addTaskButton);
            Controls.Add(taskTextBox);
            Controls.Add(fPanel);
            Name = "Form1";
            Text = "To Do List ";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel fPanel;
        private TextBox taskTextBox;
        private Button addTaskButton;
    }
}
