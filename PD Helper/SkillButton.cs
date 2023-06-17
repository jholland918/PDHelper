using PD_Helper.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PD_Helper.Form1;

namespace PD_Helper
{
    internal class SkillButton : Button
    {
        internal string SkillKey { get; private set; }
        internal PDCard Card { get; private set; }

        internal SkillButton(KeyValuePair<string, PDCard> skill) : base()
        {
            SkillKey = skill.Key;
            Card = skill.Value;

            Text = $"{Card.NAME}";
            BackColor = AppColors.GetSkillColor(Card.TYPE);
            Image = AppImages.GetSchool(Card.SCHOOL);

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Width = 300;
            TextAlign = ContentAlignment.MiddleLeft;
            ImageAlign = ContentAlignment.MiddleRight;
        }
    }
}
