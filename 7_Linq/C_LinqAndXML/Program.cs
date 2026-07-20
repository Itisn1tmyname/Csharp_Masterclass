using System.Xml.Linq;

namespace C_LinqAndXML;

class Program
{
    static void Main()
    {
        string studentsXml =
            @"<Students>
                <Student>
                    <Id>1</Id>
                    <Name>Carla</Name>
                    <Gender>female</Gender>
                    <Age>17</Age>
                    <UniversityId>1</UniversityId>
                </Student>
                <Student>
                    <Id>2</Id>
                    <Name>Toni</Name>
                    <Gender>male</Gender>
                    <Age>21</Age>
                    <UniversityId>1</UniversityId>
                </Student>
                <Student>
                    <Id>3</Id>
                    <Name>Thomas</Name>
                    <Gender>male</Gender>
                    <Age>23</Age>
                    <UniversityId>1</UniversityId>
                </Student>
                <Student>
                    <Id>4</Id>
                    <Name>Leyla</Name>
                    <Gender>female</Gender>
                    <Age>19</Age>
                    <UniversityId>2</UniversityId>
                </Student>
                <Student>
                    <Id>5</Id>
                    <Name>James</Name>
                    <Gender>divers</Gender>
                    <Age>25</Age>
                    <UniversityId>2</UniversityId>
                </Student>
                <Student>
                    <Id>6</Id>
                    <Name>Linda</Name>
                    <Gender>female</Gender>
                    <Age>22</Age>
                    <UniversityId>2</UniversityId>
                </Student>
            </Students>";

        XDocument studentsXDoc = XDocument.Parse(studentsXml);

        var students =
            from student in studentsXDoc.Descendants("Student")
            orderby student.Element("Age")!.Value
            select new
            {
                Id = student.Element("Id")!.Value,
                Name = student.Element("Name")!.Value,
                Gender = student.Element("Gender")!.Value,
                Age = student.Element("Age")!.Value,
                UniversityId = student.Element("UniversityId")!.Value
            };
        
        foreach (var student in students)
            Console.WriteLine($"{student.Id} - {student.Name,6} {$"({student.Gender})",-8} is {student.Age} years old and goes to University Nr.{student.UniversityId}");
    }
}