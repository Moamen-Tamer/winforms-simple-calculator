using System;
using System.Drawing;
using System.Windows.Forms;

class CalculatorApp: Form {
    private TextBox numbersDisplay;
    private TextBox operatorDisplay;
    private string currentValue = "";
    private string operation = "";
    private double firstNumber = 0;
    private double secondNumber = 0;
    private string theme = "dark";
    private bool canReset = false;

    private double addNumbers(double firstNumber, double secondNumber) {
        return firstNumber + secondNumber;
    }

    private double subtractNumbers(double firstNumber, double secondNumber) {
        return firstNumber - secondNumber;
    }

    private double multiplyNumbers(double firstNumber, double secondNumber) {
        return firstNumber * secondNumber;
    }

    private double divideNumbers(double firstNumber, double secondNumber) {
        return firstNumber / secondNumber;
    }

    public CalculatorApp() {
        this.Text = "Calculator";
        this.ClientSize = new Size(320, 480);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        numbersDisplay = new TextBox();
        numbersDisplay.Text = "";
        numbersDisplay.Location = new Point(87, 10);
        numbersDisplay.Size = new Size(this.ClientSize.Width - 101, 67);
        numbersDisplay.Font = new Font("Arial", 16);
        numbersDisplay.TextAlign = HorizontalAlignment.Right;
        numbersDisplay.ReadOnly = true;
        this.Controls.Add(numbersDisplay);

        operatorDisplay = new TextBox();
        operatorDisplay.Text = "operator: ";
        operatorDisplay.Location = new Point(87, 45);
        operatorDisplay.Size = new Size(this.ClientSize.Width - 101, 67);
        operatorDisplay.Font = new Font("Arial", 16);
        operatorDisplay.TextAlign = HorizontalAlignment.Left;
        operatorDisplay.BorderStyle = BorderStyle.None;
        operatorDisplay.ReadOnly = true;
        this.Controls.Add(operatorDisplay);

        int buttonSize = 68;
        int spacing = 8;
        string[] numbers = {"7", "8", "9", "4", "5", "6", "1", "2", "3", "0"};

        for (int i = 0; i < numbers.Length; i++) {
            Button btn = new Button();
            btn.Text = numbers[i];
            btn.Size = new Size(buttonSize, buttonSize);
            btn.Font = new Font("Arial", 14);

            if (i < 9) {
                int col = i % 3;
                int row = i / 3;
                btn.Location = new Point(
                    10 + (col * (buttonSize + spacing)),
                    168 + (row * (buttonSize + spacing))
                );
            } else {
                btn.Location = new Point(
                    10 + (1 * (buttonSize + spacing)),
                    168 + (3 * (buttonSize + spacing))
                );
            }

            btn.Click += numberClick;

            this.Controls.Add(btn);
        }

        string[] operators = {"÷", "×", "-", "+"};
        
        for (int i = 0; i < operators.Length; i++) {
            Button btn = new Button();
            btn.Text = operators[i];
            btn.Size = new Size(buttonSize, buttonSize);
            btn.Font = new Font("Arial", 14);

            int row = i % 4;

            btn.Location = new Point(
                10 + (3 * (buttonSize + spacing)),
                168 + ((row - 1) * (buttonSize + spacing))
            );

            btn.Click += operatorClick;

            this.Controls.Add(btn);
        }

        string[] functions = {"AC", "%", "↵"};

        for (int i = 0; i < functions.Length; i++) {
            Button btn = new Button();
            btn.Text = functions[i];
            btn.Size = new Size(buttonSize, buttonSize);
            btn.Font = new Font("Arial", 14);
            btn.Location = new Point(10 + (i * (buttonSize + spacing)),92);

            if (i == 0) {
                btn.Click += acClick;
            } else if (i == 1) {
                btn.Click += percentageClick;
            } else {
                btn.Click += removeClick;
            }

            this.Controls.Add(btn);
        }

        Button btnTheme = new Button();
        Image originalImage = Image.FromFile(@"D:\VS\WORKSHOP\light.png");
        btnTheme.Image = new Bitmap(originalImage, new Size(40, 40));
        btnTheme.Text = "";
        btnTheme.Size = new Size(buttonSize, buttonSize);
        btnTheme.Font = new Font("Arial", 14);
        btnTheme.Location = new Point(10, 10);
        btnTheme.Click += themeConvertClick;
        this.Controls.Add(btnTheme);

        Button btnDoubleZeros = new Button();
        btnDoubleZeros.Text = "00";
        btnDoubleZeros.Size = new Size(buttonSize, buttonSize);
        btnDoubleZeros.Font = new Font("Arial", 14);
        btnDoubleZeros.Location = new Point(10, 168 + (3 * (buttonSize + spacing)));
        btnDoubleZeros.Click += numberClick;
        this.Controls.Add(btnDoubleZeros);

        Button btnDecimal = new Button();
        btnDecimal.Text = ".";
        btnDecimal.Size = new Size(buttonSize, buttonSize);
        btnDecimal.Font = new Font("Arial", 14);
        btnDecimal.Location = new Point(
            10 + (2 * (buttonSize + spacing)),
            168 + (3 * (buttonSize + spacing))
        );
        btnDecimal.Click += numberClick;
        this.Controls.Add(btnDecimal);

        Button btnEqual = new Button();
        btnEqual.Text = "=";
        btnEqual.Size = new Size(buttonSize, buttonSize);
        btnEqual.Font = new Font("Arial", 14);
        btnEqual.Location = new Point(
            10 + (3 * (buttonSize + spacing)),
            168 + (3 * (buttonSize + spacing))
        );
        btnEqual.Click += equalClick;
        this.Controls.Add(btnEqual);

        currentTheme(theme);
    }

    public void currentTheme(string theme) {
        if (theme == "dark") {
            this.BackColor = ColorTranslator.FromHtml("#1e1e1e");
            numbersDisplay.BackColor = ColorTranslator.FromHtml("#292929");
            numbersDisplay.ForeColor = ColorTranslator.FromHtml("#ffe4c4");
            operatorDisplay.BackColor = ColorTranslator.FromHtml("#1e1e1e");
            operatorDisplay.ForeColor = ColorTranslator.FromHtml("#ffe4c4");
        } else {
            this.BackColor = ColorTranslator.FromHtml("#f5f5f5");
            numbersDisplay.BackColor = ColorTranslator.FromHtml("#d4d4d4");
            numbersDisplay.ForeColor = ColorTranslator.FromHtml("#4d2600");
            operatorDisplay.BackColor = ColorTranslator.FromHtml("#f5f5f5");
            operatorDisplay.ForeColor = ColorTranslator.FromHtml("#4d2600");
        }

        foreach(Control ctrl in this.Controls) {
            if (ctrl is Button btn) {
                if (theme == "dark") {
                    btn.BackColor = ColorTranslator.FromHtml("#333333");

                    if (btn.Text == "AC" || btn.Text == "↵") {
                        btn.ForeColor = ColorTranslator.FromHtml("#ff1a1a");
                    } else {
                        btn.ForeColor = ColorTranslator.FromHtml("#f5f5f5");
                    }
                } else {
                    btn.BackColor = ColorTranslator.FromHtml("#dedeff");

                    if (btn.Text == "AC" || btn.Text == "↵") {
                        btn.ForeColor = ColorTranslator.FromHtml("#ff0000" );
                    } else {
                        btn.ForeColor = ColorTranslator.FromHtml("#000000");
                    }
                }
            }
        }
    }

    public void numberClick(Object? sender, EventArgs e) {
        if (sender is not Button btn) return;

        if (currentValue == "" || currentValue == "0" || canReset == true) {
            currentValue = btn.Text;
            canReset = false;
        } else {
            currentValue += btn.Text;
        }

        numbersDisplay.Text = currentValue;
    }

    public void operatorClick(Object? sender, EventArgs e) {
        if (sender is not Button btn) return;

        firstNumber = double.Parse(currentValue);
        currentValue = "";
        operation = btn.Text;
        operatorDisplay.Text = "operator: " + operation;
    }

    public void acClick(Object? sender, EventArgs e) {
        if (sender is not Button btn) return;

        currentValue = "";
        numbersDisplay.Text = currentValue;
    }

    public void percentageClick (Object? sender, EventArgs e) {
        if (sender is not Button btn) return;
        if (currentValue == "") return;

        double val = double.Parse(currentValue);
        val /= 100;
        currentValue = val.ToString();
        numbersDisplay.Text = currentValue;
    }

    public void removeClick(Object? sender, EventArgs e) {
        if (sender is not Button btn) return;
        if (currentValue == "") return;

        currentValue = currentValue[..^1];
        numbersDisplay.Text = currentValue;
    }

    public void equalClick(Object? sender, EventArgs e) {
        if (sender is not Button btn) return;
        if (operation == "") return;

        secondNumber = double.Parse(currentValue);

        if (operation == "÷" && secondNumber == 0) currentValue = "Error";

        switch(operation) {
            case "+":
                currentValue = addNumbers(firstNumber, secondNumber).ToString();
                break;
            case "-":
                currentValue = subtractNumbers(firstNumber, secondNumber).ToString();
                break;
            case "×":
                currentValue = multiplyNumbers(firstNumber, secondNumber).ToString();
                break;
            case "÷":
                currentValue = divideNumbers(firstNumber, secondNumber).ToString();
                break;
        }

        firstNumber = secondNumber = 0;
        numbersDisplay.Text = currentValue;
        operatorDisplay.Text = "operator: ";
        canReset = true;
    }

    public void themeConvertClick(Object? sender, EventArgs e) {
        if (theme == "dark") {
            this.BackColor = ColorTranslator.FromHtml("#f5f5f5");
            numbersDisplay.BackColor = ColorTranslator.FromHtml("#d4d4d4");
            numbersDisplay.ForeColor = ColorTranslator.FromHtml("#4d2600");
            operatorDisplay.BackColor = ColorTranslator.FromHtml("#f5f5f5");
            operatorDisplay.ForeColor = ColorTranslator.FromHtml("#4d2600");
        } else {
            this.BackColor = ColorTranslator.FromHtml("#1e1e1e");
            numbersDisplay.BackColor = ColorTranslator.FromHtml("#292929");
            numbersDisplay.ForeColor = ColorTranslator.FromHtml("#ffe4c4");
            operatorDisplay.BackColor = ColorTranslator.FromHtml("#1e1e1e");
            operatorDisplay.ForeColor = ColorTranslator.FromHtml("#ffe4c4");
        }

        foreach(Control ctrl in this.Controls) {
            if (ctrl is Button btn) {
                if (theme == "dark") {
                    btn.BackColor = ColorTranslator.FromHtml("#dedeff");

                    if (btn.Text == "AC" || btn.Text == "↵") {
                        btn.ForeColor = ColorTranslator.FromHtml("#ff0000");
                    } else if (btn.Text == "") {
                        Image originalImage = Image.FromFile(@"D:\VS\WORKSHOP\dark.png");
                        btn.Image = new Bitmap(originalImage, new Size(40, 40));
                    } else {
                        btn.ForeColor = ColorTranslator.FromHtml("#000000");
                    }
                } else {
                    btn.BackColor = ColorTranslator.FromHtml("#333333");

                    if (btn.Text == "AC" || btn.Text == "↵") {
                        btn.ForeColor = ColorTranslator.FromHtml("#ff1a1a");
                    } else if (btn.Text == "") {
                        Image originalImage = Image.FromFile(@"D:\VS\WORKSHOP\light.png");
                        btn.Image = new Bitmap(originalImage, new Size(40, 40));
                    } else {
                        btn.ForeColor = ColorTranslator.FromHtml("#f5f5f5");
                    }
                }
            }
        }

        theme = theme == "dark" ? "light" : "dark";
    }

    [STAThread]
    static void Main() {
        Application.EnableVisualStyles();
        Application.Run(new CalculatorApp());
    }
}