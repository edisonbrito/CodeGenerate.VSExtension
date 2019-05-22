using Praxio.CodeGenerator.CleanArchitecture.VSExtension.Models.Attributes;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Praxio.CodeGenerator.CleanArchitecture.VSExtension.Util
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(
                                              typeof(DescriptionAttribute),
                                              false);

                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return en.ToString();
        }

        public static string GetEnumMigrationColumn(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(
                                              typeof(MigrationColumnAttribute),
                                              false);

                if (attrs != null && attrs.Length > 0)
                    return ((MigrationColumnAttribute)attrs[0]).ColumnType;
            }

            return en.ToString();
        }

        public static string GetFrontType(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(
                                              typeof(FrontTypeAttribute),
                                              false);

                if (attrs != null && attrs.Length > 0)
                    return ((FrontTypeAttribute)attrs[0]).Type;
            }

            return en.ToString();
        }
    }
}