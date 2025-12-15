using System.Windows.Forms;
using LaunchFrame.Core.Models;

namespace LaunchFrame.UI;

public partial class OptionsForm : Form
{
    private readonly List<UrlEntry> _workingUrls;

    public IReadOnlyList<UrlEntry> Urls => _workingUrls;

    public OptionsForm(IEnumerable<UrlEntry> urls)
    {
        _workingUrls = urls.Select(u => new UrlEntry { Url = u.Url, Enabled = u.Enabled }).ToList();
        InitializeComponent();
        RefreshList();
    }

    private void RefreshList()
    {
        checkedUrls.Items.Clear();
        foreach (var url in _workingUrls)
        {
            checkedUrls.Items.Add(url.Url, url.Enabled);
        }
    }

    private void OnAddUrl(object? sender, EventArgs e)
    {
        var text = txtNewUrl.Text.Trim();
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        _workingUrls.Add(new UrlEntry { Url = text, Enabled = true });
        txtNewUrl.Clear();
        RefreshList();
    }

    private void OnRemoveUrl(object? sender, EventArgs e)
    {
        if (checkedUrls.SelectedIndex < 0)
        {
            return;
        }

        _workingUrls.RemoveAt(checkedUrls.SelectedIndex);
        RefreshList();
    }

    private void OnAccept(object? sender, EventArgs e)
    {
        for (var i = 0; i < _workingUrls.Count; i++)
        {
            _workingUrls[i].Enabled = checkedUrls.GetItemChecked(i);
        }

        DialogResult = DialogResult.OK;
        Close();
    }
}
