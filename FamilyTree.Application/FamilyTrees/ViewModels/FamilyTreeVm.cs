using FamilyTree.Application.People.ViewModels;
using System.Collections.Generic;

namespace FamilyTree.Application.FamilyTrees.ViewModels
{
    public class FamilyTreeVm
    {
        public PersonVm MainPerson; // главный персонаж
        public PersonVm Parent_1; // первый родитель
        public PersonVm Parent_2; // второй родитель
        public PersonVm Grand_1_1; // первый родитель 1-го родителя (бабушка/дедушка)
        public PersonVm Grand_1_2; // второй родитель 1-го родителя (бабушка/дедушка)
        public PersonVm Grand_2_1; // первый родитель 2-го родителя (бабушка/дедушка)
        public PersonVm Grand_2_2; // второй родитель 2-го родителя (бабушка/дедушка)
        public List<PersonVm> Brothers; // братья
        public List<PersonVm> Wifes; // жены
        public PersonVm Parent_W_1; // первый родитель жены
        public PersonVm Parent_W_2; // второй родитель жены
        public PersonVm Grand_W_1_1; // первый родитель 1-го родителя жены (бабушка/дедушка жены)
        public PersonVm Grand_W_1_2; // второй родитель 1-го родителя жены (бабушка/дедушка жены)
        public PersonVm Grand_W_2_1; // первый родитель 2-го родителя жены (бабушка/дедушка жены)
        public PersonVm Grand_W_2_2; // второй родитель 2-го родителя жены (бабушка/дедушка жены)
        public List<PersonVm> Children; // дети
        public PersonVm ChildWife_2; // ребенок от второго брака

        public bool[] Grand_has_parent; // есть ли у бабушек/дедушек родители
        public bool[] Grand_has_another_child; // есть ли у бабушек/дедушек дети от другого брака
        public bool[] Parent_has_brother; // есть ли у родителей братья/сестры
        public bool[] Parent_has_another_child; // есть ли у родителей ребенок от другого брака
        public bool WifeBrother; // есть ли у жены братья/сестры           
        public bool Wife_2_has_parent; // есть ли у второй жены родители
        public bool[] BrothersSons; // есть ли у братьев дети
        public bool Child_Wife_3; // есть ли ребенок от 3-й жены
        public bool[] Wife_has_another_child; // есть ли у 1 и 2 жен дети от других браков
        public bool CountChildrenWife_2; // количество детей у 2-й жены больше 1
        public bool[] Child_has_sons; // есть ли у детей дети
        public bool Child_Another_has_sons; // есть ли у ребенка второго брака дети
    }
}
