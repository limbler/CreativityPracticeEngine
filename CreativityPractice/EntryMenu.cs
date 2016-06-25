using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreativityPractice
{
    public partial class EntryMenu : Form
    {

        public EntryMenu()
        {
            InitializeComponent();
            // Sets up the initial objects in the CheckedListBox. 
            //string[] myGenres = { "Art", "Writing", "Poetry", "Music" };
            string[] myGenres = Constants.categories;
            genreCheckedListBox.Items.AddRange(myGenres);
        }


        private void startButton_Click(object sender, EventArgs e)
        {
            // turn start button blue cuz it looks good
            startButton.ForeColor = System.Drawing.Color.Cyan;

            // find out which categories were selected
            List<string> checkedItemsList = new List<string>();
            for (int i = 0; i <= (genreCheckedListBox.Items.Count - 1); i++)
            {
                if (genreCheckedListBox.GetItemChecked(i))
                {
                    checkedItemsList.Add(genreCheckedListBox.Items[i].ToString());
                }
            }
            // if user did not select a category, warn them and exit.
            if (checkedItemsList.Count < 1)
            {
                MessageBox.Show("Please select a category!");

                return;
            }

            // otherwise, generate a prompt to match
            BasicTextPromptForm prompt = new BasicTextPromptForm();
            prompt.categories = checkedItemsList;
            PromptGenerator promptGenerator = new PromptGenerator(checkedItemsList);
            BasicTextPrompt newPrompt;
            if (speedModeBox.Checked)
            {
                //promptGenerator.generatePrompt(Constants.speedModeTimeLimit);
                prompt.speedModeLimit = Constants.speedModeTimeLimit;
                newPrompt = promptGenerator.findAPrompt(checkedItemsList, Constants.speedModeTimeLimit);
            }
            else
            {
                //promptGenerator.generatePrompt(Constants.speedModeOff);
                prompt.speedModeLimit = 0;
                newPrompt = promptGenerator.findAPrompt(checkedItemsList, 0);
            }
            if (newPrompt.ERROR == true)
            {
                Console.WriteLine("BasicTextPromptForm.generateNewPrompt() : Error - failed to make new prompt");
                MessageBox.Show("Error generating prompt. Please try again.");
                return;
            }
            //FancyTextPromptForm promptTest = new FancyTextPromptForm();
            prompt.InitializePrompt(newPrompt);
            prompt.ShowDialog();
        }

        private void mouseOverStart(object sender, EventArgs e)
        {
            startButton.ForeColor = System.Drawing.Color.Cyan;
        }
        private void mouseLeavesStart(object sender, EventArgs e)
        {
            startButton.ForeColor = System.Drawing.Color.Black;
        }
        private void createNewPromptsLabel_MouseEnter(object sender, EventArgs e)
        {
            createNewPromptsLabel.ForeColor = System.Drawing.Color.Cyan;
        }
        private void createNewPromptsLabel_MouseLeave(object sender, EventArgs e)
        {
            createNewPromptsLabel.ForeColor = System.Drawing.Color.Black;
        }

        private void createNewPromptsLabel_Click(object sender, EventArgs e)
        {
            // pop open new form 
            CreateNewPromptsForm creation = new CreateNewPromptsForm();
            if (creation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
        }

        private void speedModeHelpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In speed mode, only prompts with suggested times of less than " + Constants.speedModeTimeLimit + " minutes will be shown.");
        }


    }
}
