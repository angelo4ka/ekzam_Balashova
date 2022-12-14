using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaschetZarplatiApp.FailiDannih
{
    class PolzovatelObj
    {
        /// <summary>
        /// Наличие у текущего пользователя системы специальных прав
        /// (добавление, редактирование, удаление):
        /// true - есть супер-права (применяется к менеджеру); false - их нет (применяется к исполнителю).
        /// </summary>
        public static bool IsSyperPrava;
        public static User Polsovayel;
        /// <summary>
        /// Степень компетентности сотрудника и его положение в иерархии организации,
        /// одно из значений: “junior”, “middle”, “senior”
        /// </summary>
        public static string Rang;
    }
}
