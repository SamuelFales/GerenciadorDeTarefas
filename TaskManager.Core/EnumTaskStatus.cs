using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core
{

    //1   Em produção
    //2   Finalizada
    //3   Pendente
    //4   Suspensa

    public class Enums
    {
        public enum EnumTaskStatus
        {
            [Description("Em produção")]
            Emproducao = 1,
            [Description("Finalizada")]
            Finalizada = 2,
            [Description("Pendente")]
            Pendente = 3,
            [Description("Suspensa")]
            Suspensa = 4

        }

        public static string GetDescription(System.Enum value)
        {
            var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var descriptionAttribute =
                enumMember == null
                    ? default(DescriptionAttribute)
                    : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            return
                descriptionAttribute == null
                    ? value.ToString()
                    : descriptionAttribute.Description;
        }

    }
}
