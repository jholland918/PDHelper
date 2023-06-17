using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PD_Helper.Form1;

namespace PD_Helper
{
    public partial class SkillSelectForm : Form
    {
        private ArsenalListItem arsenalListItem;
        private PDCard card;

        public SkillSelectForm(ArsenalListItem arsenalListItem, PDCard card)
        {
            this.arsenalListItem = arsenalListItem;
            this.card = card;
            InitializeComponent();
        }
    }
}
