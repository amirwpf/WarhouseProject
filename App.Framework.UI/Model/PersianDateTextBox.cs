//using System;
//using System.Windows.Forms;

//public class PersianDateTextBox : TextBox
//{
//    private const char PlaceholderChar = '_';
//    private const int MaxLength = 8;

//    public PersianDateTextBox()
//    {
//        //this.MaxLength = MaxLength;
//        this.Text = "____/__/__";
//    }

//    protected override void OnKeyPress(KeyPressEventArgs e)
//    {
//        base.OnKeyPress(e);

//        if (char.IsDigit(e.KeyChar))
//        {
//            HandleDigitKeyPress(e.KeyChar);
//        }
//        else if (e.KeyChar == (char)Keys.Back) 
//        {
//            HandleBackspaceKeyPress();
//        }
//        else if (e.KeyChar == (char)Keys.Delete) 
//        {
//            HandleDeleteKeyPress();
//        }

//        e.Handled = true;
//    }

//    private void HandleDigitKeyPress(char digit)
//    {
//        string currentText = this.Text;
//        int indexOfPlaceholder = currentText.IndexOf(PlaceholderChar);

//        if (indexOfPlaceholder >= 0)
//        {
//            currentText = currentText.Remove(indexOfPlaceholder, 1);
//            currentText = currentText.Insert(indexOfPlaceholder, digit.ToString());


//            if (indexOfPlaceholder == 2 || indexOfPlaceholder == 5)
//            {
//                currentText = currentText.Insert(indexOfPlaceholder + 1, "/");
//            }

//            this.Text = currentText;
//        }
//    }

//    private void HandleBackspaceKeyPress()
//    {
//        string currentText = this.Text;

//        if (!string.IsNullOrEmpty(currentText) && currentText.LastIndexOf(PlaceholderChar) < MaxLength - 1)
//        {
//            currentText = PlaceholderChar + currentText.Substring(0, MaxLength - 1);

//            // Ensure there is no extra '/'
//            currentText = currentText.Replace("/", "");

//            this.Text = currentText;
//        }
//    }

//    private void HandleDeleteKeyPress()
//    {
//        string currentText = this.Text;

//        // Do not allow deleting '_' or '/'
//        currentText = currentText.Replace("_", "").Replace("/", "");

//        if (currentText.Length < MaxLength)
//        {
//            currentText = currentText + new string(PlaceholderChar, MaxLength - currentText.Length);
//        }

//        this.Text = currentText;
//    }
//}


//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Windows.Forms;

//public class PersianDateTextBox : TextBox
//{
//    private const char PlaceholderChar = '_';
//    private const int MaxLength = 10; // Increased to accommodate yyyy/mm/dd format

//    public PersianDateTextBox()
//    {
//        //this.MaxLength = MaxLength;
//        this.Text = new string(PlaceholderChar, MaxLength);
//    }

//    protected override void OnKeyPress(KeyPressEventArgs e)
//    {
//        base.OnKeyPress(e);

//        if (char.IsDigit(e.KeyChar))
//        {
//            HandleDigitKeyPress(e.KeyChar);
//        }
//        else if (e.KeyChar == (char)Keys.Back)
//        {
//            HandleBackspaceKeyPress();
//        }
//        else if (e.KeyChar == (char)Keys.Delete)
//        {
//            HandleDeleteKeyPress();
//        }

//        e.Handled = true;
//    }

//    private void HandleDigitKeyPress(char digit)
//    {
//        int selectionStart = SelectionStart;
//        string currentText = this.Text;

//        if (selectionStart < currentText.Length && currentText[selectionStart] == '/')
//        {
//            selectionStart++; // Skip over '/'
//        }

//        currentText = currentText.Remove(selectionStart, 1).Insert(selectionStart, digit.ToString());

//        if (selectionStart == 4 || selectionStart == 7)
//        {
//            if (currentText[selectionStart] != '/')
//            {
//                currentText = currentText.Insert(selectionStart, "/");
//            }
//        }

//        this.Text = currentText;
//        SelectionStart = selectionStart + 1;
//    }

//    private void HandleBackspaceKeyPress()
//    {
//        int selectionStart = SelectionStart;
//        string currentText = this.Text;

//        if (selectionStart > 0)
//        {
//            if (currentText[selectionStart - 1] == '/')
//            {
//                selectionStart--;
//            }

//            currentText = currentText.Remove(selectionStart - 1, 1).Insert(selectionStart - 1, PlaceholderChar.ToString());

//            this.Text = currentText;

//            SelectionStart = selectionStart;
//        }
//    }

//    private void HandleDeleteKeyPress()
//    {
//        int selectionStart = SelectionStart;
//        string currentText = this.Text;

//        if (selectionStart < MaxLength)
//        {
//            if (currentText[selectionStart] == '/')
//            {
//                selectionStart++;
//            }

//            currentText = currentText.Remove(selectionStart, 1).Insert(selectionStart, PlaceholderChar.ToString());

//            this.Text = currentText;

//            SelectionStart = selectionStart;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

public class PersianDateTextBox : TextBox
{
    private List<int> inputDigits;
    char[] dash;
    StringBuilder s;

    public PersianDateTextBox()
    {
        dash = new char[1];
        dash[0] = '/';
        inputDigits = new List<int>();
        this.Text = "____/__/__";
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        s = new StringBuilder();
        base.OnKeyPress(e);
        if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
        {
            e.Handled = true;
        }

        if (char.IsDigit(e.KeyChar) && inputDigits.Count<8)
        {
            inputDigits.Add(int.Parse(e.KeyChar.ToString()));
        }
        if (e.KeyChar=='\b' && inputDigits.Count > 0)
        {
            inputDigits.RemoveAt(inputDigits.Count-1);
        }

        for (int i = 0; i < 8- inputDigits.Count; i++)
        {
            s.Append('_');
        }
        foreach (var digit in inputDigits)
        {
            s.Append(digit);
        }
        //s.Append("/", 5, 1);
        //s.Append("/", 7, 1);
        s.Insert(4, '/');
        s.Insert(7, '/');
        this.Text = s.ToString();
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        base.OnKeyUp(e);
        this.Text = s.ToString();
    }
}
