//using System;
//using System.Globalization;
//using System.Windows.Forms;

//public class PersianDateTextBox : TextBox
//{
//    private DateTime? selectedDate;
//    private int currentDigitPosition;
//    private bool isTextChangedEventConnected = true;

//    public PersianDateTextBox()
//    {
//        //this.TextChanged += PersianDateTextBox_TextChanged;
//        this.KeyPress += PersianDateTextBox_KeyPress;
//        //this.GotFocus += PersianDateTextBox_GotFocus;
//        //this.LostFocus += PersianDateTextBox_LostFocus;
//        ClearText();
//    }

//    private void PersianDateTextBox_KeyPress(object sender, KeyPressEventArgs e)
//    {
//        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
//        {
//            e.Handled = true;
//        }
//    }
//    //focus my text box always is 10 digit and char number 3 anf from right are / and this textbox act like a stack that the first number enter
//    // in text box goes and replace first char of textbox from right that is _ and when second number enter shift first number one left and then 4th and goes to 10th and when delete a number it delete that enterd number and shift all number one right
//    //private void PersianDateTextBox_GotFocus(object sender, EventArgs e)
//    //{
//    //    FormatInput();
//    //}

//    //private void PersianDateTextBox_LostFocus(object sender, EventArgs e)
//    //{
//    //    FormatInput();
//    //}

//    private void PersianDateTextBox_TextChanged(object sender, EventArgs e)
//    {
//        if (!isTextChangedEventConnected)
//        {
//            return;
//        }

//        isTextChangedEventConnected = false;

//        string inputText = this.Text.Replace("_", "");

//        try
//        {
//            CultureInfo persianCulture = new CultureInfo("fa-IR");
//            DateTime persianDate = DateTime.ParseExact(inputText, "yyyy/MM/dd", persianCulture);
//            DateTime gregorianDate = persianCulture.Calendar.ToDateTime(persianDate.Year, persianDate.Month, persianDate.Day, 0, 0, 0, 0);

//            selectedDate = gregorianDate;
//        }
//        catch
//        {
//            selectedDate = null;
//        }

//        FormatInput();

//        isTextChangedEventConnected = true;
//    }

//    private void FormatInput()
//    {
//        if (selectedDate.HasValue)
//        {
//            this.Text = selectedDate.Value.ToString("yyyy/MM/dd");
//        }
//        else if (string.IsNullOrWhiteSpace(this.Text))
//        {
//            ClearText();
//        }
//        else
//        {
//            string formattedText = this.Text.PadRight(10, '_');
//            formattedText = formattedText.Insert(currentDigitPosition, "_");
//            formattedText = formattedText.Insert(4, "/").Insert(7, "/");
//            this.Text = formattedText;
//        }
//    }

//    private void ClearText()
//    {
//        this.Text = "____/__/__";
//        selectedDate = null;
//        currentDigitPosition = 0;
//    }

//    protected override void OnKeyPress(KeyPressEventArgs e)
//    {
//        base.OnKeyPress(e);

//        if (char.IsDigit(e.KeyChar))
//        {
//            HandleDigitInput(int.Parse(e.KeyChar.ToString()));
//        }
//    }

//    private void HandleDigitInput(int digit)
//    {
//        if (currentDigitPosition < 10)
//        {
//            string newText = this.Text.Remove(currentDigitPosition, 1).Insert(currentDigitPosition, digit.ToString());
//            this.Text = newText;
//            currentDigitPosition++;
//        }

//        FormatInput();
//    }
//}

//-------------------------------------------------

//using System;
//using System.Windows.Forms;

//public class DateTextBox : TextBox
//{
//    private const char PlaceholderChar = '_';
//    private const int MaxLength = 8;

//    public DateTextBox()
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
//        else if (e.KeyChar == '\b') // Backspace key
//        {
//            HandleBackspaceKeyPress();
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

//            // Ensure the 3rd and 6th characters from the right are '/'
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
//}



using System;
using System.Windows.Forms;

public class DateTextBox : TextBox
{
    private const char PlaceholderChar = '_';
    private const int MaxLength = 8;

    public DateTextBox()
    {
        //this.MaxLength = MaxLength;
        this.Text = "____/__/__";
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        base.OnKeyPress(e);

        if (char.IsDigit(e.KeyChar))
        {
            HandleDigitKeyPress(e.KeyChar);
        }
        else if (e.KeyChar == (char)Keys.Back) 
        {
            HandleBackspaceKeyPress();
        }
        else if (e.KeyChar == (char)Keys.Delete) 
        {
            HandleDeleteKeyPress();
        }

        e.Handled = true;
    }

    private void HandleDigitKeyPress(char digit)
    {
        string currentText = this.Text;
        int indexOfPlaceholder = currentText.IndexOf(PlaceholderChar);

        if (indexOfPlaceholder >= 0)
        {
            currentText = currentText.Remove(indexOfPlaceholder, 1);
            currentText = currentText.Insert(indexOfPlaceholder, digit.ToString());


            if (indexOfPlaceholder == 2 || indexOfPlaceholder == 5)
            {
                currentText = currentText.Insert(indexOfPlaceholder + 1, "/");
            }

            this.Text = currentText;
        }
    }

    private void HandleBackspaceKeyPress()
    {
        string currentText = this.Text;

        if (!string.IsNullOrEmpty(currentText) && currentText.LastIndexOf(PlaceholderChar) < MaxLength - 1)
        {
            currentText = PlaceholderChar + currentText.Substring(0, MaxLength - 1);

            // Ensure there is no extra '/'
            currentText = currentText.Replace("/", "");

            this.Text = currentText;
        }
    }

    private void HandleDeleteKeyPress()
    {
        string currentText = this.Text;

        // Do not allow deleting '_' or '/'
        currentText = currentText.Replace("_", "").Replace("/", "");

        if (currentText.Length < MaxLength)
        {
            currentText = currentText + new string(PlaceholderChar, MaxLength - currentText.Length);
        }

        this.Text = currentText;
    }
}
