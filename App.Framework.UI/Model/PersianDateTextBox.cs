using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

public class PersianDateTextBox : TextBox
{
    private DateTime _date;
    private bool _validDate;
    private List<int> _inputDigits;
    private StringBuilder _formattedText;

    public event PropertyChangedEventHandler PropertyChanged;

    public DateTime Date
    {
        get => _date;
        set
        {
            if (_date != value)
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
    }

    public bool ValidDate
    {
        get => _validDate;
        set
        {
            if (_validDate != value)
            {
                _validDate = value;
                OnPropertyChanged(nameof(ValidDate));
            }
        }
    }

    public PersianDateTextBox()
    {
        PropertyChanged += PersianDateTextBox_PropertyChanged;
        ValidDate = true;
        _date = DateTime.Now;
        _inputDigits = new List<int>();
        _formattedText = new StringBuilder();
        this.Text = $"____/__/__";
    }

    private void PersianDateTextBox_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Date))
        {
            UpdateTextFromDateTime();
        }
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        base.OnKeyPress(e);

        if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
        {
            e.Handled = true;
        }

        if (char.IsDigit(e.KeyChar) && _inputDigits.Count < 8)
        {
            _inputDigits.Add(int.Parse(e.KeyChar.ToString()));
        }

        if (e.KeyChar == '\b' && _inputDigits.Count > 0)
        {
            _inputDigits.RemoveAt(_inputDigits.Count - 1);
        }

        UpdateTextFromInputDigits();

        if (_inputDigits.Count == 8)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            var persianYear = _inputDigits[0] * 1000 + _inputDigits[1] * 100 + _inputDigits[2] * 10 + _inputDigits[3];
            var persianMonth = _inputDigits[4] * 10 + _inputDigits[5];
            var persianDay = _inputDigits[6] * 10 + _inputDigits[7];
            try
            {
                Date = new DateTime(persianYear, persianMonth, persianDay, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, persianCalendar);
                ValidDate = true;
            }
            catch
            {
                ValidDate = false;
            }
        }
    }

    private void UpdateTextFromInputDigits()
    {
        _formattedText.Clear();

        for (int i = 0; i < 8 - _inputDigits.Count; i++)
        {
            _formattedText.Append('_');
        }

        foreach (var digit in _inputDigits)
        {
            _formattedText.Append(digit);
        }

        _formattedText.Insert(4, '/');
        _formattedText.Insert(7, '/');

        this.Text = _formattedText.ToString();
    }

    private void UpdateTextFromDateTime()
    {
        PersianCalendar pc = new PersianCalendar();
        _formattedText.Clear();

        _formattedText.Append($"{pc.GetYear(Date)}/{pc.GetMonth(Date)}/{pc.GetDayOfMonth(Date)}");

        this.Text = _formattedText.ToString();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        base.OnKeyUp(e);
        this.Text = _formattedText.ToString();
    }
}
