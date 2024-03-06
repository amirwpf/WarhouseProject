using System.Windows.Forms;

public class NumericTextBox : TextBox
{


    public NumericTextBox()
    {

    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        base.OnKeyPress(e);

        if (char.IsDigit(e.KeyChar) ||  (e.KeyChar == (char)Keys.Back))
        {

        }
        else
        {
            e.Handled = true;
        }
    }
}