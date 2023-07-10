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
        internal Skill Card { get; private set; }

        internal SkillButton(Skill skill) : base()
        {
            SkillKey = skill.Hex;
            Card = skill;

            Text = $"{Card.Name}";
            BackColor = AppColors.GetSkillColor(Card.Type);
            Image = AppImages.GetSchool(Card.School);

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Width = 300;
            TextAlign = ContentAlignment.MiddleLeft;
            ImageAlign = ContentAlignment.MiddleRight;
        }
    }
}
