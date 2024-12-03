using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using DB;
using System.Windows.Forms;

namespace DemoEx.data
{   
    public static class DocumentHelper
    {
        private static Db db = new Db();

        public static void createDocument(int dealId, int ownerId, int estateId)
        {
            db.setConnectionStr(Connection.getConnectionString());
            string type = db.getValuesFromColumn($"select type from deals where id={dealId};")[0];

            var clientId = db.getIntValuesFromColumn($"select client from deals where id={dealId};")[0];

            var clientFIO = db.getValuesFromColumn($"select CONCAT(surname, ' ', name, ' ',patronymic) from clients where id={clientId};")[0].ToString();
            var ownerFIO = db.getValuesFromColumn($"select CONCAT(surname, ' ', name, ' ',patronymic) from clients where id={ownerId};")[0].ToString();
            var clientPassport = db.getValuesFromColumn($"select passport from clients where id={clientId};")[0].ToString();
            var ownerPassport = db.getValuesFromColumn($"select passport from clients where id={ownerId};")[0].ToString();
            var clientBirth = db.getDateValuesFromColumn($"select birth from clients where id={clientId};")[0];
            var ownerBirth = db.getDateValuesFromColumn($"select birth from clients where id={ownerId};")[0];
            var clientAddress = db.getValuesFromColumn($"select address from clients where id={clientId};")[0].ToString();
            var ownerAddress = db.getValuesFromColumn($"select address from clients where id={ownerId};")[0].ToString();

            var estateAddress = db.getValuesFromColumn($"select address from estate where id={estateId};")[0].ToString();
            var estateCadastral = db.getValuesFromColumn($"select cadastral from estate where id={estateId};")[0].ToString();
            var estateRooms = db.getIntValuesFromColumn($"select rooms from estate where id={estateId};")[0];
            var estateSquare = db.getIntValuesFromColumn($"select square from estate where id={estateId};")[0];
            var estatePrice = db.getIntValuesFromColumn($"select price from estate where id={estateId};")[0];

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Document wordDoc = wordApp.Documents.Add();
            wordApp.Visible = true;

            if (type == "Покупка")
            {
                Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                titleParagraph.Range.Text = "Договор купли-продажи квартиры";
                titleParagraph.Range.Font.Name = "Times New Roman";
                titleParagraph.Range.Font.Size = 24;
                titleParagraph.Range.Font.Bold = 1;
                titleParagraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                titleParagraph.Range.InsertParagraphAfter();

                Paragraph bodyParagraph = wordDoc.Content.Paragraphs.Add();
                bodyParagraph.Range.Text = $"{ownerFIO} (Ф.И.О), паспорт серии {ownerPassport.Split(' ')[0]} № {ownerPassport.Split(' ')[1]}, дата рождения {ownerBirth.ToString("D")}, зарегистрирован по адресу: {ownerAddress}, именуемый в дальнейшем \"Продавец\", с одной стороны и {clientFIO} (Ф.И.О), паспорт серии {clientPassport.Split(' ')[0]} № {clientPassport.Split(' ')[1]}, дата рождения {clientBirth.ToString("D")}, зарегистрирован по адресу: {clientAddress}, именуемый в дальнейшем \"Покупатель\", с другой стороны, далее совместно именуемые \"Стороны\", заключили настоящий договор о нижеследующем: ";
                bodyParagraph.Range.Font.Name = "Times New Roman";
                bodyParagraph.Range.Font.Size = 16;
                bodyParagraph.Range.ParagraphFormat.SpaceBefore = 24;
                bodyParagraph.Range.Font.Bold = 0;
                bodyParagraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                bodyParagraph.Range.InsertParagraphAfter();

                Paragraph subjectTitleParagraph = wordDoc.Content.Paragraphs.Add();
                subjectTitleParagraph.Range.Text = "1. Предмет договора";
                subjectTitleParagraph.Range.Font.Name = "Times New Roman";
                subjectTitleParagraph.Range.Font.Size = 20;
                subjectTitleParagraph.Range.Font.Bold = 1;
                subjectTitleParagraph.Range.ParagraphFormat.SpaceBefore = 24;
                subjectTitleParagraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                subjectTitleParagraph.Range.InsertParagraphAfter();

                Paragraph bodyParagraph2 = wordDoc.Content.Paragraphs.Add();
                bodyParagraph2.Range.Text = $"1.1 ПРОДАВЕЦ продает, а ПОКУПАТЕЛЬ покупает в собственность за цену и на условиях установленных настоящим Договором, Квартиру (в дальнейшем именуемую \"Квартира\"), имеющую кадастровый номер {estateCadastral}, находящуюся на этаже многоквартирного жилого дома.";
                bodyParagraph2.Range.Font.Name = "Times New Roman";
                bodyParagraph2.Range.Font.Size = 16;
                bodyParagraph2.Range.ParagraphFormat.SpaceBefore = 24;
                bodyParagraph2.Range.Font.Bold = 0;
                bodyParagraph2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                bodyParagraph2.Range.InsertParagraphAfter();

                Paragraph bodyParagraph3 = wordDoc.Content.Paragraphs.Add();
                bodyParagraph3.Range.Text = $"Квартира расположена по адресу: {estateAddress}.";
                bodyParagraph3.Range.Font.Name = "Times New Roman";
                bodyParagraph3.Range.Font.Size = 16;
                bodyParagraph3.Range.ParagraphFormat.SpaceBefore = 24;
                bodyParagraph3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                bodyParagraph3.Range.InsertParagraphAfter();

                Paragraph bodyParagraph4 = wordDoc.Content.Paragraphs.Add();
                bodyParagraph4.Range.Text = $"Квартира имеет следующие характеристики: Квартира состоит из {estateRooms} комнат, общая площадь Квартиры {estateSquare} кв.м.";
                bodyParagraph4.Range.Font.Name = "Times New Roman";
                bodyParagraph4.Range.Font.Size = 16;
                bodyParagraph4.Range.ParagraphFormat.SpaceBefore = 24;
                bodyParagraph4.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                bodyParagraph4.Range.InsertParagraphAfter();

                Paragraph subjectTitleParagraph2 = wordDoc.Content.Paragraphs.Add();
                subjectTitleParagraph2.Range.Text = "2. Передача квартиры";
                subjectTitleParagraph2.Range.Font.Name = "Times New Roman";
                subjectTitleParagraph2.Range.Font.Size = 20;
                subjectTitleParagraph2.Range.Font.Bold = 1;
                subjectTitleParagraph2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                subjectTitleParagraph2.Range.InsertParagraphAfter();

                Paragraph bodyParagraph5 = wordDoc.Content.Paragraphs.Add();
                bodyParagraph5.Range.Text = $"2.1 Переход права собственности на Квартиру от Продавца к Покупателю подлежит государственной регистрации в Едином государственном реестре недвижимости в порядке, установленном законодательством Российской Федерации. Покупателем становится собственником Квартиры с момента государственной регистрации.";
                bodyParagraph5.Range.Font.Name = "Times New Roman";
                bodyParagraph5.Range.Font.Size = 16;
                bodyParagraph5.Range.ParagraphFormat.SpaceBefore = 24;
                bodyParagraph5.Range.Font.Bold = 0;
                bodyParagraph5.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                bodyParagraph5.Range.InsertParagraphAfter();

                Paragraph bodyParagraph6 = wordDoc.Content.Paragraphs.Add();
                bodyParagraph6.Range.Text = $"2.2 Квартира должна быть передана Продавцом в фактическое владение Покупателя в течение 7 календарных дней с момента заключения настоящего Договора.";
                bodyParagraph6.Range.Font.Name = "Times New Roman";
                bodyParagraph6.Range.Font.Size = 16;
                bodyParagraph6.Range.ParagraphFormat.SpaceBefore = 24;
                bodyParagraph6.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                bodyParagraph6.Range.InsertParagraphAfter();

                Paragraph bodyParagraph7 = wordDoc.Content.Paragraphs.Add();
                bodyParagraph7.Range.Text = $"При передаче Квартиры Продавец обязан передать Покупателю также всю имеющуюся техническую и иную документацию на Квартиру и находящееся в ней оборудование, а также документацию и предметы, связанные с владением, эксплуатацией и использованием Квартиры (ключи, документы и т.п.)";
                bodyParagraph7.Range.Font.Name = "Times New Roman";
                bodyParagraph7.Range.Font.Size = 16;
                bodyParagraph7.Range.ParagraphFormat.SpaceBefore = 24;
                bodyParagraph7.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                bodyParagraph7.Range.InsertParagraphAfter();

                Paragraph bodyParagraph8 = wordDoc.Content.Paragraphs.Add();
                bodyParagraph8.Range.Text = $"Передача Квартиры оформляется Актом приема-передачи.";
                bodyParagraph8.Range.Font.Name = "Times New Roman";
                bodyParagraph8.Range.Font.Size = 16;
                bodyParagraph8.Range.ParagraphFormat.SpaceBefore = 24;
                bodyParagraph8.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                bodyParagraph8.Range.InsertParagraphAfter();

                Paragraph subjectTitleParagraph3 = wordDoc.Content.Paragraphs.Add();
                subjectTitleParagraph3.Range.Text = "3. Цена Квартиры. Порядок расчетов.";
                subjectTitleParagraph3.Range.Font.Name = "Times New Roman";
                subjectTitleParagraph3.Range.Font.Size = 20;
                subjectTitleParagraph3.Range.Font.Bold = 1;
                subjectTitleParagraph3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                subjectTitleParagraph3.Range.InsertParagraphAfter();

                Paragraph bodyParagraph9 = wordDoc.Content.Paragraphs.Add();
                bodyParagraph9.Range.Text = $"3.1 Стороны пришли к соглашению, что цена, за которую Квартира продается и которую Покупатель обязан уплатить Продавцу, составляет {estatePrice} рублей.";
                bodyParagraph9.Range.Font.Name = "Times New Roman";
                bodyParagraph9.Range.Font.Size = 16;
                bodyParagraph9.Range.ParagraphFormat.SpaceBefore = 24;
                bodyParagraph9.Range.Font.Bold = 0;
                bodyParagraph9.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                bodyParagraph9.Range.InsertParagraphAfter();

                Paragraph bodyParagraph10 = wordDoc.Content.Paragraphs.Add();
                bodyParagraph10.Range.Text = $"3.2 В подтверждение получения денежных средств в сумме, указанной в п. 3.2 настоящего Договора, Продавец передает Покупателю расписку в получении соответствующей суммы. ";
                bodyParagraph10.Range.Font.Name = "Times New Roman";
                bodyParagraph10.Range.Font.Size = 16;
                bodyParagraph10.Range.ParagraphFormat.SpaceBefore = 24;
                bodyParagraph10.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                bodyParagraph10.Range.InsertParagraphAfter();

                Paragraph subjectTitleParagraph4 = wordDoc.Content.Paragraphs.Add();
                subjectTitleParagraph4.Range.Text = "4. Заверения Сторон.";
                subjectTitleParagraph4.Range.Font.Name = "Times New Roman";
                subjectTitleParagraph4.Range.Font.Size = 20;
                subjectTitleParagraph4.Range.Font.Bold = 1;
                subjectTitleParagraph4.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                subjectTitleParagraph4.Range.InsertParagraphAfter();

                Paragraph bodyParagraph11 = wordDoc.Content.Paragraphs.Add();
                bodyParagraph11.Range.Font.Bold = 0;
                bodyParagraph11.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                bodyParagraph11.Range.Font.Size = 16;
                bodyParagraph11.Range.Text = $"4.1. Продавец гарантирует и заверяет, что:\r\n4.1.1. Квартира принадлежит Продавцу на праве собственности.\r\n4.1.2. Квартира не обременена правами других лиц, в залоге, в споре, под арестом или под запретом не находится, не продана и не обещана быть проданной третьим лицам, не имеет каких-либо иных обременений.\r\n4.1.3. Квартира не имеет существенных недостатков или скрытых дефектов, которые могут в значительной степени повлиять на возможность пользования Квартирой и на ее эксплуатационные характеристики.\r\n4.1.4. Предоставленные Продавцом Выписка из Единого государственного реестра недвижимости, а также иные документы и информация являются достоверными.";
                bodyParagraph11.Range.Font.Name = "Times New Roman";
                bodyParagraph11.Range.ParagraphFormat.SpaceBefore = 24;
                bodyParagraph11.Range.InsertParagraphAfter();

                Paragraph bodyParagraph12 = wordDoc.Content.Paragraphs.Add();
                bodyParagraph12.Range.Font.Size = 16;
                bodyParagraph12.Range.Font.Bold = 0;
                bodyParagraph12.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                bodyParagraph12.Range.Text = $"4.2. Покупатель гарантирует и заверяет, что:\r\n4.2.1. До заключения настоящего Договора Покупатель визуально осмотрел Квартиру, ознакомился с ее основными конструктивными и техническими элементами и особенностями, а также с ее эксплуатационным и техническим состоянием.\r\nПокупатель удовлетворен состоянием Квартиры, каких либо дефектов и недостатков, о которых Покупателю не было сообщено, Покупателем не обнаружено.\r\nНа основании изложенного в настоящем пункте Договора Покупатель принял решение о приобретении Квартиры на условиях, установленных настоящим Договором.";
                bodyParagraph12.Range.Font.Name = "Times New Roman";
                bodyParagraph12.Range.ParagraphFormat.SpaceBefore = 24;
                bodyParagraph12.Range.InsertParagraphAfter();
            }
            else if (type == "Аренда")
            {
            }
            wordDoc.Close();
            wordApp.Quit();
        }
    }
}
