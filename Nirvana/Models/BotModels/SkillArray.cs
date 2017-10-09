using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nirvana.Models.BotModels
{
    public class SkillArray
    {
        private static List<int> skills_for_buf = new List<int> {
            #region мист
            (int)SkillEnum.Цветочный_вихрь,
            #endregion
            #region сикер
            (int)SkillEnum.Железная_плоть,
            (int)SkillEnum.Светлая_железная_плоть,
            (int)SkillEnum.Темная_железная_плоть,
            (int)SkillEnum.Магия_клинка_вселенной,
            (int)SkillEnum.Техника_черного_воина,
            (int)SkillEnum.Светлая_техника_черного_воина,
            (int)SkillEnum.Темная_техника_черного_воина,
            #endregion
            #region вар
            (int)SkillEnum.Аура_стали,
            (int)SkillEnum.Светлая_аура_стали,
            (int)SkillEnum.Темная_аура_стали,
            #endregion
            #region маг
            (int)SkillEnum.Божественный_ледяной_доспех,
            (int)SkillEnum.Демонический_ледяной_доспех,
            (int)SkillEnum.Ледяной_доспех,
            (int)SkillEnum.Светлый_ледяной_доспех,
            (int)SkillEnum.Темный_ледяной_доспех,
            (int)SkillEnum.Светлое_обморожение,
            (int)SkillEnum.Темное_обморожение,
            (int)SkillEnum.Божественное_обморожение,
            (int)SkillEnum.Демоническое_обморожение,
            (int)SkillEnum.Обморожение,
            #endregion
            #region танк
            (int)SkillEnum.Светлый_рев_главы_стаи,
            (int)SkillEnum.Темный_рев_главы_стаи,
            (int)SkillEnum.Рев_главы_стаи,
            (int)SkillEnum.Невероятная_сила,
            (int)SkillEnum.Светлая_невероятная_сила,
            (int)SkillEnum.Темная_невероятная_сила,
            #endregion
            #region дру
            (int)SkillEnum.Стена_шипов,
            (int)SkillEnum.Светлая_стена_шипов,
            (int)SkillEnum.Темная_стена_шипов,
            (int)SkillEnum.Наряд_из_цветов,
            (int)SkillEnum.Светлый_наряд_из_цветов,
            (int)SkillEnum.Темный_наряд_из_цветов,
            #endregion
            #region прист
            (int)SkillEnum.Милость_богов,
            (int)SkillEnum.Оплот_духа,
            (int)SkillEnum.Мудрость_небес,
            (int)SkillEnum.Благословенный_символ,
            (int)SkillEnum.Оплот_тела,
            #endregion
            #region лук
            #endregion
            #region син
            (int)SkillEnum.Метка_крови,
            (int)SkillEnum.Светлая_метка_крови,
            (int)SkillEnum.Темная_метка_крови,
            (int)SkillEnum.Светлая_печать_бешеного_волка,
            #endregion
            #region шам
            #endregion
            #region призрак
            (int)SkillEnum.Лунный_стих,
            (int)SkillEnum.Светлый_лунный_стих,
            (int)SkillEnum.Темный_лунный_стих
            #endregion
            #region коса
            #endregion
        };
        private static List<int> skillsForChangeForm = new List<int>
        {
            (int)ChangeFormSkill.Крылья_Ночной_танец,
            (int)ChangeFormSkill.Ночной_танец,
            (int)ChangeFormSkill.Облик_тигра,
            (int)ChangeFormSkill.Обращение_в_лисицу,
            (int)ChangeFormSkill.Светлое_обращение_в_лисицу,
            (int)ChangeFormSkill.Светлое_обращение_в_лисицу_усиленное,
            (int)ChangeFormSkill.Светлый_облик_тигра,
            (int)ChangeFormSkill.Светлый_облик_тигра_усиленное,
            (int)ChangeFormSkill.Темное_обращение_в_лисицу,
            (int)ChangeFormSkill.Темное_обращение_в_лисицу_усиленное,
            (int)ChangeFormSkill.Темный_облик_тигра,
            (int)ChangeFormSkill.Темный_облик_тигра_усиленное
        };

        public List<Skill> My_skills_for_buf { get; set; }
        public List<Skill> My_other_skills { get; set; }
        public Skill ChangeForm { get; set; }
        public Skill ShamansCall { get; set; }

        public SkillArray(IntPtr oph)
        {
            My_skills_for_buf = new List<Skill>();
            //анализируем доступные скиллы
            int skillCount = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsSkillsCount);

            for (int s = 0; s < skillCount; s++)
            {
                int id = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsToIdSkill(s));
                if (skillsForChangeForm.Contains(id))
                {
                    ChangeForm = new Skill(id, s);
                    continue;
                }
                if (id == (int)SkillEnum.Призыв)
                {
                    ShamansCall = new Skill(id, s);
                    continue;
                }
                if (skills_for_buf.Contains(id))
                {
                    My_skills_for_buf.Add(new Skill(id, s));
                    continue;
                }
                else
                    My_other_skills.Add(new Skill(id, s));

            }
        }
    }
}
