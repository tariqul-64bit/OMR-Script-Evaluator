using System.Windows.Forms;

namespace OMRCHECK

{

    public partial class Form1 : Form

    {

        public Form1()

        {

            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)

        {

            //Open and Read The Answer

            OpenFileDialog Select_Answer_File = new OpenFileDialog();

            Select_Answer_File.ShowDialog();

            StreamReader SR = new StreamReader(Select_Answer_File.FileName);

            string Answer = SR.ReadToEnd();



            // Set Answer on the text Box.

            {

                textBox51.Text = Answer[0].ToString();

                textBox52.Text = Answer[1].ToString();

                textBox53.Text = Answer[2].ToString();

                ...

                ...

                ...

                textBox98.Text = Answer[47].ToString();

                textBox99.Text = Answer[48].ToString();

                textBox100.Text = Answer[49].ToString();

            }

        }



        private void button2_Click(object sender, EventArgs e)

        {

            OpenFileDialog omrsheet = new OpenFileDialog();

            //Previewing on the PictureBox

            {

                if (omrsheet.ShowDialog() == DialogResult.OK)

                {

                    //pictureBox1.Image = new Bitmap(omrsheet.FileName);

                    pictureBox1.ImageLocation = omrsheet.FileName;

                }

            }



            //*** processing the image ***

            Bitmap img = new Bitmap(omrsheet.FileName);

            string[] answers = new string[51];

            int i, j;

            for (i = 0; i < 51; i++) answers[i] = "X";



            for (i = 1058; i < 2950; i += 77)

            {

                for (j = 358; j < 1128 ; j++)

                {

                    Color pixel = img.GetPixel(j, i);

                    if (pixel.R < 100 && pixel.G < 100 && pixel.B < 100)

                    {

                        //Console.WriteLine(i + " " + j + " ");

                        int question_number = ((i - 1058) / 77) + 1;

                        string single_answer = "X";

                        if (j < 385) single_answer = "A";

                        else if (j < 643) single_answer = "B";

                        else if (j < 884) single_answer = "C";

                        else if (j < 1128) single_answer = "D";

                        answers[question_number] = single_answer;

                        break;

                    }

                }

            }



            for (i = 1058; i < 2950; i += 77)

            {

                for (j = 1455; j < 2250; j++)

                {

                    Color pixel = img.GetPixel(j, i);

                    if (pixel.R < 100 && pixel.G < 100 && pixel.B < 100)

                    {

                        //Console.WriteLine(i + " " + j + " ");

                        int question_number = ((i - 1058) / 77) + 25+1;

                        string single_answer = "X";

                        if (j < 1480) single_answer = "A";

                        else if (j < 1740) single_answer = "B";

                        else if (j < 1980) single_answer = "C";

                        else if (j < 2230) single_answer = "D";

                        answers[question_number] = single_answer;

                        break;

                    }

                }

            }



            string Mark = "";

            for( int it=1;it<51;it++)

            {  

                Mark += answers[it];  

            }

            //MessageBox.Show(Mark);

            //string Mark = "ABCDDBCAXXXXABCDDBCAXXXXABCDDBCAXXXXABCDDBCAXXXXAB";



            //Set Student's Mark on the Text Box

            {

                textBox101.Text = Mark[0].ToString();

                textBox102.Text = Mark[1].ToString();

                textBox103.Text = Mark[2].ToString();

                ...

                ...

                ...

                textBox148.Text = Mark[47].ToString();

                textBox149.Text = Mark[48].ToString();

                textBox150.Text = Mark[49].ToString();

            }



            // Highlighting Cells GreenYellow, Khaki, Tomato

            {



                if (textBox101.Text.ToString() == "X") textBox101.BackColor = Color.Khaki;

                else if (textBox101.Text.ToString() == textBox51.Text.ToString()) textBox101.BackColor = Color.GreenYellow;

                else textBox101.BackColor = Color.Tomato;



                if (textBox102.Text.ToString() == "X") textBox102.BackColor = Color.Khaki;

                else if (textBox102.Text.ToString() == textBox52.Text.ToString()) textBox102.BackColor = Color.GreenYellow;

                else textBox102.BackColor = Color.Tomato;



                if (textBox103.Text.ToString() == "X") textBox103.BackColor = Color.Khaki;

                else if (textBox103.Text.ToString() == textBox53.Text.ToString()) textBox103.BackColor = Color.GreenYellow;

                else textBox103.BackColor = Color.Tomato;



                ...

                ...

                ...



                ...

                ...

                ...



                if (textBox149.Text.ToString() == "X") textBox149.BackColor = Color.Khaki;

                else if (textBox149.Text.ToString() == textBox99.Text.ToString()) textBox149.BackColor = Color.GreenYellow;

                else textBox149.BackColor = Color.Tomato;



                if (textBox150.Text.ToString() == "X") textBox150.BackColor = Color.Khaki;

                else if (textBox150.Text.ToString() == textBox100.Text.ToString()) textBox150.BackColor = Color.GreenYellow;

                else textBox150.BackColor = Color.Tomato;

            }



            //Calculate Result & Show Stat on Screen

            {

                int correct = 0, incorrect = 0, no_attempt = 0;

                var allTextBoxes = this.GetChildControls<TextBox>();

                foreach (TextBox tb in allTextBoxes)

                {

                    if (tb.BackColor == Color.GreenYellow) correct++;

                    else if (tb.BackColor == Color.Tomato) incorrect++;

                    else if (tb.BackColor == Color.Khaki) no_attempt++;

                }



                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;

                richTextBox2.SelectionAlignment = HorizontalAlignment.Right;

                richTextBox3.SelectionAlignment = HorizontalAlignment.Right;



                richTextBox1.Text = "Correct Answer :      " + correct.ToString() + " ( " + (correct*100/50).ToString() + "% )   ";

                richTextBox2.Text = "InCorrect Answer :      " + incorrect.ToString() + " ( " + (incorrect * 100 / 50).ToString() + "% )   ";

                richTextBox3.Text = "Not Marked :      " + no_attempt.ToString() + " ( " + (no_attempt * 100 / 50).ToString() + "% )   ";



                FileInfo F = new FileInfo(omrsheet.FileName);

                richTextBox4.Text = "Roll: " + F.Name.Substring(0,4);

            }

        }

    }

 }



//to iterate through textboxes i'm using this extension method

public static class Extensions

{

    public static IEnumerable<TControl> GetChildControls<TControl>(this Control control) where TControl : Control

    {

        var children = (control.Controls != null) ? control.Controls.OfType<TControl>() : Enumerable.Empty<TControl>();

        return children.SelectMany(c => GetChildControls<TControl>(c)).Concat(children);

    }

}