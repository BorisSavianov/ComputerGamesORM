namespace ComputerGamesORM.WinForms;

#nullable disable

public sealed partial class MainForm
{
    private System.ComponentModel.IContainer components;
    private BindingSource _gamesBindingSource;
    private ErrorProvider _errorProvider;
    private TableLayoutPanel _rootLayout;
    private TableLayoutPanel _headerLayout;
    private TableLayoutPanel _searchLayout;
    private TableLayoutPanel _contentLayout;
    private TableLayoutPanel _detailsLayout;
    private FlowLayoutPanel _topActionsPanel;
    private FlowLayoutPanel _editActionsPanel;
    private Label _titleLabel;
    private Label _subtitleLabel;
    private Label _recordCountLabel;
    private Label _modeLabel;
    private Label _idLabel;
    private Label _nameLabel;
    private Label _descriptionLabel;
    private TextBox _searchTextBox;
    private TextBox _idTextBox;
    private TextBox _nameTextBox;
    private TextBox _descriptionTextBox;
    private ComputerGamesORM.WinForms.Ui.RoundedButton _searchButton;
    private ComputerGamesORM.WinForms.Ui.RoundedButton _refreshButton;
    private ComputerGamesORM.WinForms.Ui.RoundedButton _addButton;
    private ComputerGamesORM.WinForms.Ui.RoundedButton _editButton;
    private ComputerGamesORM.WinForms.Ui.RoundedButton _deleteButton;
    private ComputerGamesORM.WinForms.Ui.RoundedButton _saveButton;
    private ComputerGamesORM.WinForms.Ui.RoundedButton _cancelButton;
    private ComputerGamesORM.WinForms.Ui.RoundedPanel _detailsPanel;
    private DataGridView _gamesGrid;
    private DataGridViewTextBoxColumn _idColumn;
    private DataGridViewTextBoxColumn _nameColumn;
    private DataGridViewTextBoxColumn _descriptionColumn;
    private StatusStrip _statusStrip;
    private ToolStripStatusLabel _statusLabel;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        _gamesBindingSource = new BindingSource(components);
        _errorProvider = new ErrorProvider(components);
        _rootLayout = new TableLayoutPanel();
        _headerLayout = new TableLayoutPanel();
        _titleLabel = new Label();
        _subtitleLabel = new Label();
        _recordCountLabel = new Label();
        _searchLayout = new TableLayoutPanel();
        _searchTextBox = new TextBox();
        _searchButton = new ComputerGamesORM.WinForms.Ui.RoundedButton();
        _refreshButton = new ComputerGamesORM.WinForms.Ui.RoundedButton();
        _contentLayout = new TableLayoutPanel();
        _gamesGrid = new DataGridView();
        _idColumn = new DataGridViewTextBoxColumn();
        _nameColumn = new DataGridViewTextBoxColumn();
        _descriptionColumn = new DataGridViewTextBoxColumn();
        _detailsPanel = new ComputerGamesORM.WinForms.Ui.RoundedPanel();
        _detailsLayout = new TableLayoutPanel();
        _modeLabel = new Label();
        _idLabel = new Label();
        _idTextBox = new TextBox();
        _nameLabel = new Label();
        _nameTextBox = new TextBox();
        _descriptionLabel = new Label();
        _descriptionTextBox = new TextBox();
        _topActionsPanel = new FlowLayoutPanel();
        _addButton = new ComputerGamesORM.WinForms.Ui.RoundedButton();
        _editButton = new ComputerGamesORM.WinForms.Ui.RoundedButton();
        _deleteButton = new ComputerGamesORM.WinForms.Ui.RoundedButton();
        _editActionsPanel = new FlowLayoutPanel();
        _saveButton = new ComputerGamesORM.WinForms.Ui.RoundedButton();
        _cancelButton = new ComputerGamesORM.WinForms.Ui.RoundedButton();
        _statusStrip = new StatusStrip();
        _statusLabel = new ToolStripStatusLabel();
        ((System.ComponentModel.ISupportInitialize)_gamesBindingSource).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_errorProvider).BeginInit();
        _rootLayout.SuspendLayout();
        _headerLayout.SuspendLayout();
        _searchLayout.SuspendLayout();
        _contentLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_gamesGrid).BeginInit();
        _detailsPanel.SuspendLayout();
        _detailsLayout.SuspendLayout();
        _topActionsPanel.SuspendLayout();
        _editActionsPanel.SuspendLayout();
        _statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // _errorProvider
        // 
        _errorProvider.ContainerControl = this;
        // 
        // _rootLayout
        // 
        _rootLayout.BackColor = Color.FromArgb(15, 23, 42);
        _rootLayout.ColumnCount = 1;
        _rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _rootLayout.Controls.Add(_headerLayout, 0, 0);
        _rootLayout.Controls.Add(_searchLayout, 0, 1);
        _rootLayout.Controls.Add(_contentLayout, 0, 2);
        _rootLayout.Controls.Add(_statusStrip, 0, 3);
        _rootLayout.Dock = DockStyle.Fill;
        _rootLayout.Location = new Point(0, 0);
        _rootLayout.Margin = new Padding(3, 4, 3, 4);
        _rootLayout.Name = "_rootLayout";
        _rootLayout.Padding = new Padding(32, 32, 32, 24);
        _rootLayout.RowCount = 4;
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 83F));
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        _rootLayout.Size = new Size(1353, 1015);
        _rootLayout.TabIndex = 0;
        // 
        // _headerLayout
        // 
        _headerLayout.BackColor = Color.FromArgb(15, 23, 42);
        _headerLayout.ColumnCount = 2;
        _headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 206F));
        _headerLayout.Controls.Add(_titleLabel, 0, 0);
        _headerLayout.Controls.Add(_subtitleLabel, 0, 1);
        _headerLayout.Controls.Add(_recordCountLabel, 1, 0);
        _headerLayout.Dock = DockStyle.Fill;
        _headerLayout.Location = new Point(35, 36);
        _headerLayout.Margin = new Padding(3, 4, 3, 4);
        _headerLayout.Name = "_headerLayout";
        _headerLayout.RowCount = 2;
        _headerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 67F));
        _headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _headerLayout.Size = new Size(1283, 112);
        _headerLayout.TabIndex = 0;
        // 
        // _titleLabel
        // 
        _titleLabel.Dock = DockStyle.Fill;
        _titleLabel.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
        _titleLabel.ForeColor = Color.FromArgb(248, 250, 252);
        _titleLabel.Location = new Point(3, 0);
        _titleLabel.Name = "_titleLabel";
        _titleLabel.Size = new Size(1071, 67);
        _titleLabel.TabIndex = 0;
        _titleLabel.Text = "Game Catalog";
        _titleLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _subtitleLabel
        // 
        _subtitleLabel.Dock = DockStyle.Fill;
        _subtitleLabel.Font = new Font("Segoe UI", 10F);
        _subtitleLabel.ForeColor = Color.FromArgb(148, 163, 184);
        _subtitleLabel.Location = new Point(3, 67);
        _subtitleLabel.Name = "_subtitleLabel";
        _subtitleLabel.Size = new Size(1071, 45);
        _subtitleLabel.TabIndex = 1;
        _subtitleLabel.Text = "Manage games and descriptions with a clean desktop workflow.";
        // 
        // _recordCountLabel
        // 
        _recordCountLabel.Dock = DockStyle.Fill;
        _recordCountLabel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        _recordCountLabel.ForeColor = Color.FromArgb(96, 165, 250);
        _recordCountLabel.Location = new Point(1080, 0);
        _recordCountLabel.Name = "_recordCountLabel";
        _headerLayout.SetRowSpan(_recordCountLabel, 2);
        _recordCountLabel.Size = new Size(200, 112);
        _recordCountLabel.TabIndex = 2;
        _recordCountLabel.Text = "0 records";
        _recordCountLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // _searchLayout
        // 
        _searchLayout.BackColor = Color.FromArgb(15, 23, 42);
        _searchLayout.ColumnCount = 3;
        _searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
        _searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135F));
        _searchLayout.Controls.Add(_searchTextBox, 0, 0);
        _searchLayout.Controls.Add(_searchButton, 1, 0);
        _searchLayout.Controls.Add(_refreshButton, 2, 0);
        _searchLayout.Dock = DockStyle.Fill;
        _searchLayout.Location = new Point(35, 156);
        _searchLayout.Margin = new Padding(3, 4, 3, 4);
        _searchLayout.Name = "_searchLayout";
        _searchLayout.Padding = new Padding(0, 11, 0, 13);
        _searchLayout.RowCount = 1;
        _searchLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _searchLayout.Size = new Size(1283, 75);
        _searchLayout.TabIndex = 1;
        // 
        // _searchTextBox
        // 
        _searchTextBox.BackColor = Color.FromArgb(2, 6, 23);
        _searchTextBox.BorderStyle = BorderStyle.FixedSingle;
        _searchTextBox.Dock = DockStyle.Fill;
        _searchTextBox.Font = new Font("Segoe UI", 10F);
        _searchTextBox.ForeColor = Color.FromArgb(226, 232, 240);
        _searchTextBox.Location = new Point(0, 11);
        _searchTextBox.Margin = new Padding(0, 0, 14, 0);
        _searchTextBox.Name = "_searchTextBox";
        _searchTextBox.PlaceholderText = "Search by ID, name, or description";
        _searchTextBox.Size = new Size(1006, 30);
        _searchTextBox.TabIndex = 0;
        // 
        // _searchButton
        // 
        _searchButton.BackColor = Color.FromArgb(37, 99, 235);
        _searchButton.Dock = DockStyle.Fill;
        _searchButton.FlatAppearance.BorderSize = 0;
        _searchButton.FlatStyle = FlatStyle.Flat;
        _searchButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _searchButton.ForeColor = Color.White;
        _searchButton.Location = new Point(1020, 11);
        _searchButton.Margin = new Padding(0, 0, 11, 0);
        _searchButton.Name = "_searchButton";
        _searchButton.Size = new Size(117, 51);
        _searchButton.TabIndex = 1;
        _searchButton.Text = "Search";
        _searchButton.UseVisualStyleBackColor = false;
        // 
        // _refreshButton
        // 
        _refreshButton.BackColor = Color.FromArgb(51, 65, 85);
        _refreshButton.Dock = DockStyle.Fill;
        _refreshButton.FlatAppearance.BorderSize = 0;
        _refreshButton.FlatStyle = FlatStyle.Flat;
        _refreshButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _refreshButton.ForeColor = Color.White;
        _refreshButton.Location = new Point(1148, 11);
        _refreshButton.Margin = new Padding(0);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.Size = new Size(135, 51);
        _refreshButton.TabIndex = 2;
        _refreshButton.Text = "Refresh";
        _refreshButton.UseVisualStyleBackColor = false;
        // 
        // _contentLayout
        // 
        _contentLayout.BackColor = Color.FromArgb(15, 23, 42);
        _contentLayout.ColumnCount = 2;
        _contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
        _contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
        _contentLayout.Controls.Add(_gamesGrid, 0, 0);
        _contentLayout.Controls.Add(_detailsPanel, 1, 0);
        _contentLayout.Dock = DockStyle.Fill;
        _contentLayout.Location = new Point(35, 239);
        _contentLayout.Margin = new Padding(3, 4, 3, 4);
        _contentLayout.Name = "_contentLayout";
        _contentLayout.RowCount = 1;
        _contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _contentLayout.Size = new Size(1283, 708);
        _contentLayout.TabIndex = 2;
        // 
        // _gamesGrid
        // 
        _gamesGrid.AllowUserToAddRows = false;
        _gamesGrid.AllowUserToDeleteRows = false;
        _gamesGrid.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle1.ForeColor = Color.FromArgb(226, 232, 240);
        _gamesGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        _gamesGrid.AutoGenerateColumns = false;
        _gamesGrid.BackgroundColor = Color.FromArgb(17, 24, 39);
        _gamesGrid.BorderStyle = BorderStyle.FixedSingle;
        _gamesGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        _gamesGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 41, 59);
        dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(241, 245, 249);
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(30, 41, 59);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(241, 245, 249);
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        _gamesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        _gamesGrid.ColumnHeadersHeight = 38;
        _gamesGrid.Columns.AddRange(new DataGridViewColumn[] { _idColumn, _nameColumn, _descriptionColumn });
        _gamesGrid.DataSource = _gamesBindingSource;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(17, 24, 39);
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(226, 232, 240);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(37, 99, 235);
        dataGridViewCellStyle3.SelectionForeColor = Color.White;
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        _gamesGrid.DefaultCellStyle = dataGridViewCellStyle3;
        _gamesGrid.Dock = DockStyle.Fill;
        _gamesGrid.EnableHeadersVisualStyles = false;
        _gamesGrid.GridColor = Color.FromArgb(51, 65, 85);
        _gamesGrid.Location = new Point(0, 0);
        _gamesGrid.Margin = new Padding(0, 0, 21, 0);
        _gamesGrid.MultiSelect = false;
        _gamesGrid.Name = "_gamesGrid";
        _gamesGrid.ReadOnly = true;
        _gamesGrid.RowHeadersVisible = false;
        _gamesGrid.RowHeadersWidth = 51;
        _gamesGrid.RowTemplate.Height = 34;
        _gamesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gamesGrid.Size = new Size(774, 708);
        _gamesGrid.TabIndex = 0;
        // 
        // _idColumn
        // 
        _idColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        _idColumn.DataPropertyName = "Id";
        _idColumn.HeaderText = "ID";
        _idColumn.MinimumWidth = 70;
        _idColumn.Name = "_idColumn";
        _idColumn.ReadOnly = true;
        _idColumn.Width = 80;
        // 
        // _nameColumn
        // 
        _nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        _nameColumn.DataPropertyName = "Name";
        _nameColumn.FillWeight = 35F;
        _nameColumn.HeaderText = "Game";
        _nameColumn.MinimumWidth = 180;
        _nameColumn.Name = "_nameColumn";
        _nameColumn.ReadOnly = true;
        // 
        // _descriptionColumn
        // 
        _descriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        _descriptionColumn.DataPropertyName = "Description";
        _descriptionColumn.FillWeight = 65F;
        _descriptionColumn.HeaderText = "Description";
        _descriptionColumn.MinimumWidth = 260;
        _descriptionColumn.Name = "_descriptionColumn";
        _descriptionColumn.ReadOnly = true;
        // 
        // _detailsPanel
        // 
        _detailsPanel.BackColor = Color.FromArgb(17, 24, 39);
        _detailsPanel.BorderColor = Color.FromArgb(51, 65, 85);
        _detailsPanel.BorderRadius = 16;
        _detailsPanel.Controls.Add(_detailsLayout);
        _detailsPanel.Dock = DockStyle.Fill;
        _detailsPanel.Location = new Point(798, 0);
        _detailsPanel.Margin = new Padding(3, 0, 0, 0);
        _detailsPanel.Name = "_detailsPanel";
        _detailsPanel.Padding = new Padding(27, 32, 27, 29);
        _detailsPanel.Size = new Size(485, 708);
        _detailsPanel.TabIndex = 1;
        // 
        // _detailsLayout
        // 
        _detailsLayout.BackColor = Color.FromArgb(17, 24, 39);
        _detailsLayout.ColumnCount = 1;
        _detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _detailsLayout.Controls.Add(_modeLabel, 0, 0);
        _detailsLayout.Controls.Add(_idLabel, 0, 1);
        _detailsLayout.Controls.Add(_idTextBox, 0, 2);
        _detailsLayout.Controls.Add(_nameLabel, 0, 3);
        _detailsLayout.Controls.Add(_nameTextBox, 0, 4);
        _detailsLayout.Controls.Add(_descriptionLabel, 0, 5);
        _detailsLayout.Controls.Add(_descriptionTextBox, 0, 6);
        _detailsLayout.Controls.Add(_topActionsPanel, 0, 7);
        _detailsLayout.Controls.Add(_editActionsPanel, 0, 8);
        _detailsLayout.Dock = DockStyle.Fill;
        _detailsLayout.Location = new Point(27, 32);
        _detailsLayout.Margin = new Padding(3, 4, 3, 4);
        _detailsLayout.Name = "_detailsLayout";
        _detailsLayout.RowCount = 9;
        _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 67F));
        _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 69F));
        _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 69F));
        _detailsLayout.Size = new Size(431, 647);
        _detailsLayout.TabIndex = 0;
        // 
        // _modeLabel
        // 
        _modeLabel.Dock = DockStyle.Fill;
        _modeLabel.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
        _modeLabel.ForeColor = Color.FromArgb(248, 250, 252);
        _modeLabel.Location = new Point(3, 0);
        _modeLabel.Name = "_modeLabel";
        _modeLabel.Size = new Size(425, 67);
        _modeLabel.TabIndex = 0;
        _modeLabel.Text = "Details";
        _modeLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _idLabel
        // 
        _idLabel.Dock = DockStyle.Fill;
        _idLabel.ForeColor = Color.FromArgb(148, 163, 184);
        _idLabel.Location = new Point(3, 67);
        _idLabel.Name = "_idLabel";
        _idLabel.Size = new Size(425, 32);
        _idLabel.TabIndex = 1;
        _idLabel.Text = "ID";
        _idLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _idTextBox
        // 
        _idTextBox.BackColor = Color.FromArgb(15, 23, 42);
        _idTextBox.BorderStyle = BorderStyle.FixedSingle;
        _idTextBox.Dock = DockStyle.Fill;
        _idTextBox.ForeColor = Color.FromArgb(226, 232, 240);
        _idTextBox.Location = new Point(3, 103);
        _idTextBox.Margin = new Padding(3, 4, 3, 4);
        _idTextBox.Name = "_idTextBox";
        _idTextBox.ReadOnly = true;
        _idTextBox.Size = new Size(425, 27);
        _idTextBox.TabIndex = 2;
        // 
        // _nameLabel
        // 
        _nameLabel.Dock = DockStyle.Fill;
        _nameLabel.ForeColor = Color.FromArgb(148, 163, 184);
        _nameLabel.Location = new Point(3, 155);
        _nameLabel.Name = "_nameLabel";
        _nameLabel.Size = new Size(425, 32);
        _nameLabel.TabIndex = 3;
        _nameLabel.Text = "Game name";
        _nameLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _nameTextBox
        // 
        _nameTextBox.BackColor = Color.FromArgb(15, 23, 42);
        _nameTextBox.BorderStyle = BorderStyle.FixedSingle;
        _nameTextBox.Dock = DockStyle.Fill;
        _nameTextBox.ForeColor = Color.FromArgb(226, 232, 240);
        _nameTextBox.Location = new Point(3, 191);
        _nameTextBox.Margin = new Padding(3, 4, 3, 4);
        _nameTextBox.MaxLength = 200;
        _nameTextBox.Name = "_nameTextBox";
        _nameTextBox.Size = new Size(425, 27);
        _nameTextBox.TabIndex = 4;
        // 
        // _descriptionLabel
        // 
        _descriptionLabel.Dock = DockStyle.Fill;
        _descriptionLabel.ForeColor = Color.FromArgb(148, 163, 184);
        _descriptionLabel.Location = new Point(3, 243);
        _descriptionLabel.Name = "_descriptionLabel";
        _descriptionLabel.Size = new Size(425, 32);
        _descriptionLabel.TabIndex = 5;
        _descriptionLabel.Text = "Description";
        _descriptionLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // _descriptionTextBox
        // 
        _descriptionTextBox.BackColor = Color.FromArgb(15, 23, 42);
        _descriptionTextBox.BorderStyle = BorderStyle.FixedSingle;
        _descriptionTextBox.Dock = DockStyle.Fill;
        _descriptionTextBox.ForeColor = Color.FromArgb(226, 232, 240);
        _descriptionTextBox.Location = new Point(3, 279);
        _descriptionTextBox.Margin = new Padding(3, 4, 3, 4);
        _descriptionTextBox.MaxLength = 2000;
        _descriptionTextBox.Multiline = true;
        _descriptionTextBox.Name = "_descriptionTextBox";
        _descriptionTextBox.ScrollBars = ScrollBars.Vertical;
        _descriptionTextBox.Size = new Size(425, 226);
        _descriptionTextBox.TabIndex = 6;
        // 
        // _topActionsPanel
        // 
        _topActionsPanel.BackColor = Color.FromArgb(17, 24, 39);
        _topActionsPanel.Controls.Add(_addButton);
        _topActionsPanel.Controls.Add(_editButton);
        _topActionsPanel.Controls.Add(_deleteButton);
        _topActionsPanel.Dock = DockStyle.Fill;
        _topActionsPanel.Location = new Point(3, 513);
        _topActionsPanel.Margin = new Padding(3, 4, 3, 4);
        _topActionsPanel.Name = "_topActionsPanel";
        _topActionsPanel.Padding = new Padding(0, 9, 0, 0);
        _topActionsPanel.Size = new Size(425, 61);
        _topActionsPanel.TabIndex = 7;
        // 
        // _addButton
        // 
        _addButton.BackColor = Color.FromArgb(37, 99, 235);
        _addButton.FlatAppearance.BorderSize = 0;
        _addButton.FlatStyle = FlatStyle.Flat;
        _addButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _addButton.ForeColor = Color.White;
        _addButton.Location = new Point(3, 13);
        _addButton.Margin = new Padding(3, 4, 3, 4);
        _addButton.Name = "_addButton";
        _addButton.Size = new Size(101, 45);
        _addButton.TabIndex = 0;
        _addButton.Text = "Add";
        _addButton.UseVisualStyleBackColor = false;
        // 
        // _editButton
        // 
        _editButton.BackColor = Color.FromArgb(14, 116, 144);
        _editButton.FlatAppearance.BorderSize = 0;
        _editButton.FlatStyle = FlatStyle.Flat;
        _editButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _editButton.ForeColor = Color.White;
        _editButton.Location = new Point(110, 13);
        _editButton.Margin = new Padding(3, 4, 3, 4);
        _editButton.Name = "_editButton";
        _editButton.Size = new Size(101, 45);
        _editButton.TabIndex = 1;
        _editButton.Text = "Edit";
        _editButton.UseVisualStyleBackColor = false;
        // 
        // _deleteButton
        // 
        _deleteButton.BackColor = Color.FromArgb(220, 38, 38);
        _deleteButton.FlatAppearance.BorderSize = 0;
        _deleteButton.FlatStyle = FlatStyle.Flat;
        _deleteButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _deleteButton.ForeColor = Color.White;
        _deleteButton.Location = new Point(217, 13);
        _deleteButton.Margin = new Padding(3, 4, 3, 4);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.Size = new Size(101, 45);
        _deleteButton.TabIndex = 2;
        _deleteButton.Text = "Delete";
        _deleteButton.UseVisualStyleBackColor = false;
        // 
        // _editActionsPanel
        // 
        _editActionsPanel.BackColor = Color.FromArgb(17, 24, 39);
        _editActionsPanel.Controls.Add(_saveButton);
        _editActionsPanel.Controls.Add(_cancelButton);
        _editActionsPanel.Dock = DockStyle.Fill;
        _editActionsPanel.Location = new Point(3, 582);
        _editActionsPanel.Margin = new Padding(3, 4, 3, 4);
        _editActionsPanel.Name = "_editActionsPanel";
        _editActionsPanel.Padding = new Padding(0, 9, 0, 0);
        _editActionsPanel.Size = new Size(425, 61);
        _editActionsPanel.TabIndex = 8;
        // 
        // _saveButton
        // 
        _saveButton.BackColor = Color.FromArgb(22, 163, 74);
        _saveButton.FlatAppearance.BorderSize = 0;
        _saveButton.FlatStyle = FlatStyle.Flat;
        _saveButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _saveButton.ForeColor = Color.White;
        _saveButton.Location = new Point(3, 13);
        _saveButton.Margin = new Padding(3, 4, 3, 4);
        _saveButton.Name = "_saveButton";
        _saveButton.Size = new Size(123, 45);
        _saveButton.TabIndex = 0;
        _saveButton.Text = "Save";
        _saveButton.UseVisualStyleBackColor = false;
        // 
        // _cancelButton
        // 
        _cancelButton.BackColor = Color.FromArgb(100, 116, 139);
        _cancelButton.FlatAppearance.BorderSize = 0;
        _cancelButton.FlatStyle = FlatStyle.Flat;
        _cancelButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _cancelButton.ForeColor = Color.White;
        _cancelButton.Location = new Point(132, 13);
        _cancelButton.Margin = new Padding(3, 4, 3, 4);
        _cancelButton.Name = "_cancelButton";
        _cancelButton.Size = new Size(123, 45);
        _cancelButton.TabIndex = 1;
        _cancelButton.Text = "Cancel";
        _cancelButton.UseVisualStyleBackColor = false;
        // 
        // _statusStrip
        // 
        _statusStrip.BackColor = Color.FromArgb(15, 23, 42);
        _statusStrip.Dock = DockStyle.Fill;
        _statusStrip.ImageScalingSize = new Size(20, 20);
        _statusStrip.Items.AddRange(new ToolStripItem[] { _statusLabel });
        _statusStrip.Location = new Point(32, 951);
        _statusStrip.Name = "_statusStrip";
        _statusStrip.Padding = new Padding(1, 0, 16, 0);
        _statusStrip.Size = new Size(1289, 40);
        _statusStrip.SizingGrip = false;
        _statusStrip.TabIndex = 3;
        // 
        // _statusLabel
        // 
        _statusLabel.ForeColor = Color.FromArgb(148, 163, 184);
        _statusLabel.Name = "_statusLabel";
        _statusLabel.Size = new Size(1272, 34);
        _statusLabel.Spring = true;
        _statusLabel.Text = "Ready.";
        _statusLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(15, 23, 42);
        ClientSize = new Size(1353, 1015);
        Controls.Add(_rootLayout);
        Font = new Font("Segoe UI", 9F);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(1209, 891);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Computer Games ORM";
        ((System.ComponentModel.ISupportInitialize)_gamesBindingSource).EndInit();
        ((System.ComponentModel.ISupportInitialize)_errorProvider).EndInit();
        _rootLayout.ResumeLayout(false);
        _rootLayout.PerformLayout();
        _headerLayout.ResumeLayout(false);
        _searchLayout.ResumeLayout(false);
        _searchLayout.PerformLayout();
        _contentLayout.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_gamesGrid).EndInit();
        _detailsPanel.ResumeLayout(false);
        _detailsLayout.ResumeLayout(false);
        _detailsLayout.PerformLayout();
        _topActionsPanel.ResumeLayout(false);
        _editActionsPanel.ResumeLayout(false);
        _statusStrip.ResumeLayout(false);
        _statusStrip.PerformLayout();
        ResumeLayout(false);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (components != null)
            {
                components.Dispose();
            }
        }

        base.Dispose(disposing);
    }
}
