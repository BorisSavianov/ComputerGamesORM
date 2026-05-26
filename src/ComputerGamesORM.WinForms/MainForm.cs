using System.ComponentModel;
using ComputerGamesORM.Business;

namespace ComputerGamesORM.WinForms;

public sealed partial class MainForm : Form
{
    private readonly IGameService _gameService;

    private int? _selectedGameId;
    private bool _isEditMode;
    private bool _isBindingSelection;
    private bool _gridColumnsConfigured;

    public MainForm()
        : this(new DesignTimeGameService())
    {
    }

    public MainForm(IGameService gameService)
    {
        _gameService = gameService;
        InitializeComponent();
        ConfigureRuntimeBehavior();

        if (IsDesignerHosted() || gameService is DesignTimeGameService)
        {
            LoadDesignTimeData();
        }
        else
        {
            Load += MainForm_Load;
        }
    }

    private async void MainForm_Load(object? sender, EventArgs e)
    {
        await ExecuteUiActionAsync(() => LoadGamesAsync(), "Ready.");
    }

    private void ConfigureRuntimeBehavior()
    {
        _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

        ConfigureGamesGrid();

        _searchTextBox.KeyDown += SearchTextBox_KeyDown;
        _searchButton.Click += SearchButton_Click;
        _refreshButton.Click += RefreshButton_Click;
        _gamesGrid.SelectionChanged += GamesGrid_SelectionChanged;
        _gamesGrid.DoubleClick += GamesGrid_DoubleClick;
        _addButton.Click += AddButton_Click;
        _editButton.Click += EditButton_Click;
        _deleteButton.Click += DeleteButton_Click;
        _saveButton.Click += SaveButton_Click;
        _cancelButton.Click += CancelButton_Click;

        SetEditorReadOnly(true);
        UpdateCommandState();
    }

    private void LoadDesignTimeData()
    {
        ConfigureGamesGrid();
        _gamesBindingSource.DataSource = new BindingList<GameDto>(new List<GameDto>
        {
            new GameDto(1, "Half-Life 2", "A story-driven first-person shooter focused on physics and resistance."),
            new GameDto(2, "Portal 2", "A puzzle game built around portal mechanics and test chambers."),
            new GameDto(3, "Hades", "A fast roguelike action game with repeated escape attempts.")
        });

        _recordCountLabel.Text = "3 records";
        BindSelectedRow();
    }

    private async void SearchTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        e.SuppressKeyPress = true;
        await ExecuteUiActionAsync(() => LoadGamesAsync(_searchTextBox.Text), "Search complete.");
    }

    private async void SearchButton_Click(object? sender, EventArgs e)
    {
        await ExecuteUiActionAsync(() => LoadGamesAsync(_searchTextBox.Text), "Search complete.");
    }

    private async void RefreshButton_Click(object? sender, EventArgs e)
    {
        _searchTextBox.Clear();
        await ExecuteUiActionAsync(() => LoadGamesAsync(), "Data refreshed.");
    }

    private void GamesGrid_SelectionChanged(object? sender, EventArgs e)
    {
        BindSelectedRow();
    }

    private void GamesGrid_DoubleClick(object? sender, EventArgs e)
    {
        StartEditMode();
    }

    private void AddButton_Click(object? sender, EventArgs e)
    {
        StartCreateMode();
    }

    private void EditButton_Click(object? sender, EventArgs e)
    {
        StartEditMode();
    }

    private async void DeleteButton_Click(object? sender, EventArgs e)
    {
        await ExecuteUiActionAsync(DeleteSelectedAsync, "Record deleted.");
    }

    private async void SaveButton_Click(object? sender, EventArgs e)
    {
        await ExecuteUiActionAsync(SaveAsync, "Record saved.");
    }

    private void CancelButton_Click(object? sender, EventArgs e)
    {
        CancelEditMode();
    }

    private async Task LoadGamesAsync(string? searchText = null)
    {
        SetBusy(true);
        try
        {
            ConfigureGamesGrid();
            _isBindingSelection = true;
            var previousId = _selectedGameId;
            var games = await _gameService.GetAllAsync(searchText);
            _gamesBindingSource.DataSource = new BindingList<GameDto>(games.ToList());
            _recordCountLabel.Text = games.Count == 1 ? "1 record" : $"{games.Count} records";

            if (previousId.HasValue && SelectGame(previousId.Value))
            {
                return;
            }

            if (_gamesGrid.Rows.Count > 0)
            {
                _gamesGrid.Rows[0].Selected = true;
                _gamesGrid.CurrentCell = _gamesGrid.Rows[0].Cells[0];
            }
            else
            {
                ClearEditor();
            }
        }
        finally
        {
            _isBindingSelection = false;
            BindSelectedRow();
            SetBusy(false);
        }
    }

    private void ConfigureGamesGrid()
    {
        if (_gridColumnsConfigured)
        {
            return;
        }

        _gamesGrid.SuspendLayout();
        _gamesGrid.AutoGenerateColumns = false;
        _gamesGrid.Columns.Clear();

        _idColumn = new DataGridViewTextBoxColumn
        {
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            DataPropertyName = nameof(GameDto.Id),
            HeaderText = "ID",
            MinimumWidth = 70,
            Name = "_idColumn",
            ReadOnly = true,
            Width = 80
        };

        _nameColumn = new DataGridViewTextBoxColumn
        {
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            DataPropertyName = nameof(GameDto.Name),
            FillWeight = 35F,
            HeaderText = "Game",
            MinimumWidth = 180,
            Name = "_nameColumn",
            ReadOnly = true
        };

        _descriptionColumn = new DataGridViewTextBoxColumn
        {
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            DataPropertyName = nameof(GameDto.Description),
            FillWeight = 65F,
            HeaderText = "Description",
            MinimumWidth = 260,
            Name = "_descriptionColumn",
            ReadOnly = true
        };

        _gamesGrid.Columns.AddRange(_idColumn, _nameColumn, _descriptionColumn);
        _gamesGrid.BorderStyle = BorderStyle.FixedSingle;
        _gamesGrid.Visible = true;
        _gamesGrid.BringToFront();
        _gamesGrid.ResumeLayout();

        _gridColumnsConfigured = true;
    }

    private void BindSelectedRow()
    {
        if (_isBindingSelection || _isEditMode)
        {
            return;
        }

        if (_gamesBindingSource.Current is not GameDto game)
        {
            ClearEditor();
            return;
        }

        _selectedGameId = game.Id;
        _modeLabel.Text = "Details";
        _idTextBox.Text = game.Id.ToString();
        _nameTextBox.Text = game.Name;
        _descriptionTextBox.Text = game.Description;
        SetEditorReadOnly(true);
        UpdateCommandState();
    }

    private void StartCreateMode()
    {
        _isEditMode = true;
        _selectedGameId = null;
        _modeLabel.Text = "New game";
        _idTextBox.Clear();
        _nameTextBox.Clear();
        _descriptionTextBox.Clear();
        _errorProvider.Clear();
        SetEditorReadOnly(false);
        UpdateCommandState();
        _nameTextBox.Focus();
        ShowStatus("Creating a new record.");
    }

    private void StartEditMode()
    {
        if (_selectedGameId is null)
        {
            ShowStatus("Select a record before editing.", true);
            return;
        }

        _isEditMode = true;
        _modeLabel.Text = "Edit game";
        _errorProvider.Clear();
        SetEditorReadOnly(false);
        UpdateCommandState();
        _nameTextBox.Focus();
        _nameTextBox.SelectAll();
        ShowStatus("Editing selected record.");
    }

    private void CancelEditMode()
    {
        _isEditMode = false;
        _errorProvider.Clear();
        SetEditorReadOnly(true);
        BindSelectedRow();
        UpdateCommandState();
        ShowStatus("Edit cancelled.");
    }

    private async Task<bool> SaveAsync()
    {
        if (!ValidateEditor())
        {
            ShowStatus("Please fix validation errors before saving.", true);
            return false;
        }

        var model = new GameEditModel(_nameTextBox.Text, _descriptionTextBox.Text);
        int selectedId;

        if (_selectedGameId.HasValue)
        {
            var updated = await _gameService.UpdateAsync(_selectedGameId.Value, model);
            if (!updated)
            {
                ShowStatus("The selected game no longer exists.", true);
                await LoadGamesAsync(_searchTextBox.Text);
                return false;
            }

            selectedId = _selectedGameId.Value;
        }
        else
        {
            selectedId = await _gameService.CreateAsync(model);
        }

        _isEditMode = false;
        SetEditorReadOnly(true);
        await LoadGamesAsync(_searchTextBox.Text);
        SelectGame(selectedId);
        UpdateCommandState();
        return true;
    }

    private async Task<bool> DeleteSelectedAsync()
    {
        if (_selectedGameId is null)
        {
            ShowStatus("Select a record before deleting.", true);
            return false;
        }

        var result = MessageBox.Show(
            this,
            $"Delete '{_nameTextBox.Text}' and its description?",
            "Confirm delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);

        if (result != DialogResult.Yes)
        {
            ShowStatus("Delete cancelled.");
            return false;
        }

        var deleted = await _gameService.DeleteAsync(_selectedGameId.Value);
        if (!deleted)
        {
            ShowStatus("The selected game no longer exists.", true);
            _selectedGameId = null;
            await LoadGamesAsync(_searchTextBox.Text);
            return false;
        }

        _selectedGameId = null;
        await LoadGamesAsync(_searchTextBox.Text);
        return true;
    }

    private bool ValidateEditor()
    {
        _errorProvider.Clear();
        var isValid = true;

        if (string.IsNullOrWhiteSpace(_nameTextBox.Text))
        {
            _errorProvider.SetError(_nameTextBox, "Game name is required.");
            isValid = false;
        }
        else if (_nameTextBox.Text.Trim().Length > GameService.MaxNameLength)
        {
            _errorProvider.SetError(_nameTextBox, $"Game name must be up to {GameService.MaxNameLength} characters.");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(_descriptionTextBox.Text))
        {
            _errorProvider.SetError(_descriptionTextBox, "Description is required.");
            isValid = false;
        }
        else if (_descriptionTextBox.Text.Trim().Length > GameService.MaxDescriptionLength)
        {
            _errorProvider.SetError(_descriptionTextBox, $"Description must be up to {GameService.MaxDescriptionLength} characters.");
            isValid = false;
        }

        return isValid;
    }

    private bool SelectGame(int id)
    {
        foreach (DataGridViewRow row in _gamesGrid.Rows)
        {
            if (row.DataBoundItem is GameDto game && game.Id == id)
            {
                row.Selected = true;
                _gamesGrid.CurrentCell = row.Cells[0];
                _selectedGameId = id;
                BindSelectedRow();
                return true;
            }
        }

        return false;
    }

    private void ClearEditor()
    {
        _selectedGameId = null;
        _modeLabel.Text = "Details";
        _idTextBox.Clear();
        _nameTextBox.Clear();
        _descriptionTextBox.Clear();
        _errorProvider.Clear();
        SetEditorReadOnly(true);
        UpdateCommandState();
    }

    private void SetEditorReadOnly(bool readOnly)
    {
        _nameTextBox.ReadOnly = readOnly;
        _descriptionTextBox.ReadOnly = readOnly;
        _idTextBox.BackColor = Color.FromArgb(15, 23, 42);
        _idTextBox.ForeColor = Color.FromArgb(226, 232, 240);
        _nameTextBox.BackColor = readOnly ? Color.FromArgb(15, 23, 42) : Color.FromArgb(2, 6, 23);
        _descriptionTextBox.BackColor = readOnly ? Color.FromArgb(15, 23, 42) : Color.FromArgb(2, 6, 23);
        _nameTextBox.ForeColor = Color.FromArgb(226, 232, 240);
        _descriptionTextBox.ForeColor = Color.FromArgb(226, 232, 240);
    }

    private void UpdateCommandState()
    {
        var hasSelection = _selectedGameId.HasValue;
        _addButton.Enabled = !_isEditMode;
        _editButton.Enabled = !_isEditMode && hasSelection;
        _deleteButton.Enabled = !_isEditMode && hasSelection;
        _saveButton.Enabled = _isEditMode;
        _cancelButton.Enabled = _isEditMode;
        _gamesGrid.Enabled = !_isEditMode;
        _searchButton.Enabled = !_isEditMode;
        _refreshButton.Enabled = !_isEditMode;
        _searchTextBox.Enabled = true;
        _searchTextBox.ReadOnly = _isEditMode;
        _searchTextBox.BackColor = Color.FromArgb(2, 6, 23);
        _searchTextBox.ForeColor = Color.FromArgb(226, 232, 240);
    }

    private void SetBusy(bool isBusy)
    {
        UseWaitCursor = isBusy;
        _gamesGrid.Enabled = !isBusy && !_isEditMode;
        _searchButton.Enabled = !isBusy && !_isEditMode;
        _refreshButton.Enabled = !isBusy && !_isEditMode;
        _addButton.Enabled = !isBusy && !_isEditMode;
        _editButton.Enabled = !isBusy && !_isEditMode && _selectedGameId.HasValue;
        _deleteButton.Enabled = !isBusy && !_isEditMode && _selectedGameId.HasValue;
        _saveButton.Enabled = !isBusy && _isEditMode;
        _cancelButton.Enabled = !isBusy && _isEditMode;
        _searchTextBox.ReadOnly = isBusy || _isEditMode;
        _searchTextBox.Enabled = true;
        _searchTextBox.BackColor = Color.FromArgb(2, 6, 23);
        _searchTextBox.ForeColor = Color.FromArgb(226, 232, 240);
        ShowStatus(isBusy ? "Working..." : "Ready.");
    }

    private async Task ExecuteUiActionAsync(Func<Task> action, string successMessage)
    {
        try
        {
            await action();
            ShowStatus(successMessage);
        }
        catch (ArgumentException ex)
        {
            ShowStatus(ex.Message, true);
            MessageBox.Show(this, ex.Message, "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (InvalidOperationException ex)
        {
            ShowStatus(ex.Message, true);
            MessageBox.Show(this, ex.Message, "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            ShowStatus("Unexpected error. See details dialog.", true);
            MessageBox.Show(this, ex.Message, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task ExecuteUiActionAsync(Func<Task<bool>> action, string successMessage)
    {
        try
        {
            var succeeded = await action();
            if (succeeded)
            {
                ShowStatus(successMessage);
            }
        }
        catch (ArgumentException ex)
        {
            ShowStatus(ex.Message, true);
            MessageBox.Show(this, ex.Message, "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (InvalidOperationException ex)
        {
            ShowStatus(ex.Message, true);
            MessageBox.Show(this, ex.Message, "Operation failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            ShowStatus("Unexpected error. See details dialog.", true);
            MessageBox.Show(this, ex.Message, "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ShowStatus(string message, bool isError = false)
    {
        _statusLabel.Text = message;
        _statusLabel.ForeColor = isError ? Color.FromArgb(248, 113, 113) : Color.FromArgb(148, 163, 184);
    }

    private static bool IsDesignerHosted()
    {
        return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
    }
}
