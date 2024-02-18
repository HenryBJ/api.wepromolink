using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePromoLink.Shared.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("01cc70d3-d906-4082-93f3-634e8169fa5f"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("1d9f30e3-428a-4dbe-ab17-980ff2ba99d1"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("2fb10b88-c042-4348-bd2e-ec6ca75f3645"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("3612f2de-787b-4f67-987f-181d82ec7f86"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("50061763-2048-418f-a10a-c6b24c7dd2e5"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("7b67f7b6-4d90-406b-8e37-724b23f572a3"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("b4deaea6-382b-44ee-bfdc-7fa5b1175932"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("c4ec5857-0583-4a64-8f24-4dc73397ec7b"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("00cb5ad4-db39-49bc-9086-56c964f08ddd"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("13efa766-abfd-43d3-8d8a-dd5c4b26bf7f"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("14e4045f-e49e-42ba-8626-6c8bd29a2ca1"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("1e80f4e9-0c9d-4762-880b-eab13f6ce7bf"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("1f5975ab-d264-405e-9f84-6ac561bf2043"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("3f25d65e-db90-4198-aaa0-0bb934ce7a2f"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("4289cfa8-8fa9-439a-9112-e71b88f5f012"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("43043f7a-84fb-4d64-81b4-d943f3eabbce"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("4e272e2d-5714-41ba-90c1-5c2c8744d0b9"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("512a0c27-962b-4fe4-b2f3-f4ef2bd1ab2c"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("5e36d09d-e7f2-4205-8b67-db0d345c5679"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("8f46e82d-cd5d-461c-a7ee-8da37ca66bb9"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("94005dee-685a-4357-9aa9-d7b708bf9126"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("a080d796-9e78-4875-ac89-2ad515dd3273"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("a5e9a232-8537-44c2-938f-cfda436efed7"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("b1db5655-1315-4f86-b96c-70a2fe650af7"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("bdc5cde1-4ff1-470e-9f8d-7895ea5efe1a"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("bfca7e19-bfd9-4fc0-8052-48a358f4c6d0"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("c8a44738-fb17-475b-983c-ff809fac2a1a"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("cf7ba3b6-7a05-4334-b9ab-01cb65dae9e4"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("e0e437dd-bab7-4aa3-bc38-771d87a0cc92"));

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("24a2fa48-6f7f-4eb1-affe-8b2d7a6c8d32"));

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("bdcb831d-eeb6-4ff9-aebe-734fbbe40ce5"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("5c3e48f4-9c88-4fd5-8aaf-b1193abc3243"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("82125427-cf18-4654-81c7-371ec2c76a5d"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("9a5eec8c-e058-4d8f-9531-2ed736c0257c"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("9f735dbe-e3a1-4d79-b95f-4499ac874a5e"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("bea9806c-3447-4de1-924f-fd9fd1dc79f2"));

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "Annually", "AnnualyPriceId", "Commission", "Discount", "ExternalId", "Level", "Metadata", "Monthly", "MonthlyPriceId", "Order", "PaymentMethod", "Tag", "Title" },
                values: new object[,]
                {
                    { new Guid("0e30c8e6-ad20-4eea-a43d-09266dd018d8"), 0m, "", 0m, 0m, "I0pjPXUAg9FW", 2, null, 4.99m, "price_1OkwiHC26XBdqsojpgor0QaV", 3, "stripe", "Popular", "Professional" },
                    { new Guid("612d8a00-e033-4023-964b-f030446ad156"), 0m, "", 0.01m, 0m, "A5ND47ppvjGg", 1, null, 0m, "price_1Ol0LGC26XBdqsojWuT9BKvb", 2, "stripe", "", "Basic" }
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "Group", "Value" },
                values: new object[,]
                {
                    { new Guid("3865574d-5903-47d3-843f-52fdae8ec179"), 1, "What motivates you to use a platform like WePromoLink?" },
                    { new Guid("41a26f05-d33f-49c3-b8d7-03f16964f185"), 5, "What additional features or improvements would you like to see on the WePromoLink platform?" },
                    { new Guid("58bf6843-e255-4236-a00f-e71ca119b139"), 3, "Do you prefer a monthly subscription fee or paying a commission on earnings?" },
                    { new Guid("70aff2c3-143b-4bc5-b292-1da9b533bab3"), 2, "Which payment methods do you prefer for platform subscriptions and earnings withdrawals?" },
                    { new Guid("98479597-72fe-454f-9daa-d3f2c96fe5b2"), 4, "How useful do you find the WePromoLink platform for your advertising needs?" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionFeatures",
                columns: new[] { "Id", "BoolValue", "Name", "SubscriptionPlanModelId", "Value" },
                values: new object[,]
                {
                    { new Guid("1a2e9f6d-8dbb-4824-ac7b-61952ba98edc"), true, "Contain ads", new Guid("612d8a00-e033-4023-964b-f030446ad156"), "" },
                    { new Guid("39bc8015-ac4b-49ea-b38e-33cc93019f88"), false, "Contain ads", new Guid("0e30c8e6-ad20-4eea-a43d-09266dd018d8"), "" },
                    { new Guid("98eb2e10-1d7c-4836-9405-db267a95d5eb"), false, "Campaigns", new Guid("612d8a00-e033-4023-964b-f030446ad156"), "Unlimited" },
                    { new Guid("a30d5cb6-00ff-4ad0-92ba-1319c00e75ca"), false, "Commission per click", new Guid("0e30c8e6-ad20-4eea-a43d-09266dd018d8"), "U$0.00" },
                    { new Guid("a3181025-de65-46db-a34b-44648611c142"), false, "Links", new Guid("612d8a00-e033-4023-964b-f030446ad156"), "Unlimited" },
                    { new Guid("c1e369e0-e0e7-4c0b-9eed-9953bb3cbd53"), false, "Campaigns", new Guid("0e30c8e6-ad20-4eea-a43d-09266dd018d8"), "Unlimited" },
                    { new Guid("c437f58d-36ef-4281-95d9-e44f05bd607c"), false, "Links", new Guid("0e30c8e6-ad20-4eea-a43d-09266dd018d8"), "Unlimited" },
                    { new Guid("f6bf6471-2ca2-46ad-aa8f-cb89c9c1beb0"), false, "Commission per click", new Guid("612d8a00-e033-4023-964b-f030446ad156"), "U$0.01" }
                });

            migrationBuilder.InsertData(
                table: "SurveyAnswers",
                columns: new[] { "Id", "SurveyQuestionModelId", "Value" },
                values: new object[,]
                {
                    { new Guid("0adac7ee-0ea8-4d37-8c7b-85405e9d469a"), new Guid("70aff2c3-143b-4bc5-b292-1da9b533bab3"), "Stripe" },
                    { new Guid("0ec8a3eb-4a7f-4381-b047-a1898febfaab"), new Guid("58bf6843-e255-4236-a00f-e71ca119b139"), "I'm not sure" },
                    { new Guid("151c1c0c-8246-4df0-85ff-94afba2540c2"), new Guid("70aff2c3-143b-4bc5-b292-1da9b533bab3"), "Bank transfer" },
                    { new Guid("1e188ef8-554b-4787-88a8-e31612650171"), new Guid("70aff2c3-143b-4bc5-b292-1da9b533bab3"), "Credit/Debit card" },
                    { new Guid("1f81aa3b-0f18-4bb8-890e-9bcfed191d2d"), new Guid("41a26f05-d33f-49c3-b8d7-03f16964f185"), "I'm not sure" },
                    { new Guid("20fe51bb-0542-44b7-80f1-7e344dcab02a"), new Guid("70aff2c3-143b-4bc5-b292-1da9b533bab3"), "PayPal" },
                    { new Guid("218e834a-b7fb-4d38-bfda-7ea4f692eb4d"), new Guid("98479597-72fe-454f-9daa-d3f2c96fe5b2"), "Very useful" },
                    { new Guid("28f77e3d-bcd1-4b79-b886-f9400d2c145f"), new Guid("3865574d-5903-47d3-843f-52fdae8ec179"), "Connecting with other users and businesses" },
                    { new Guid("4097affd-71b3-40ed-be5c-d6ccf52beb86"), new Guid("3865574d-5903-47d3-843f-52fdae8ec179"), "Earning money through affiliate marketing" },
                    { new Guid("52b2d50d-89c8-4b7c-8c82-6fd0b58f22d9"), new Guid("98479597-72fe-454f-9daa-d3f2c96fe5b2"), "Not very useful" },
                    { new Guid("5365c62b-80fe-4fd6-a33b-d2f0c37c14dd"), new Guid("3865574d-5903-47d3-843f-52fdae8ec179"), "Promoting my products or services" },
                    { new Guid("6379b10c-a5dc-4b9d-a38f-46c0d3693a52"), new Guid("98479597-72fe-454f-9daa-d3f2c96fe5b2"), "Not useful at all" },
                    { new Guid("87fb7e30-3f38-462f-b22f-0748de25c575"), new Guid("41a26f05-d33f-49c3-b8d7-03f16964f185"), "Integration with other advertising platforms" },
                    { new Guid("90ce0366-5306-4146-8305-4c5b1b3318cc"), new Guid("98479597-72fe-454f-9daa-d3f2c96fe5b2"), "Somewhat useful" },
                    { new Guid("98b87916-5815-401e-8954-40301b55c222"), new Guid("98479597-72fe-454f-9daa-d3f2c96fe5b2"), "Extremely useful" },
                    { new Guid("9c417a48-9b50-4143-a2e7-91ab0d56ce5c"), new Guid("58bf6843-e255-4236-a00f-e71ca119b139"), "Monthly subscription fee" },
                    { new Guid("a266a8d9-e116-479f-b3f4-d525ed90cdda"), new Guid("58bf6843-e255-4236-a00f-e71ca119b139"), "Commission on earnings" },
                    { new Guid("c13e915f-1c19-4337-89e6-8f2ca2073bc5"), new Guid("41a26f05-d33f-49c3-b8d7-03f16964f185"), "More campaign customization options" },
                    { new Guid("d3839e74-b362-43a6-92e3-73b403a368f5"), new Guid("3865574d-5903-47d3-843f-52fdae8ec179"), "Exploring new advertising opportunities" },
                    { new Guid("f67bcc17-d3cd-440a-93e0-9d0782bda853"), new Guid("41a26f05-d33f-49c3-b8d7-03f16964f185"), "Improved user interface and navigation" },
                    { new Guid("fe5a02e6-55da-4d21-9e7e-15c795b06545"), new Guid("41a26f05-d33f-49c3-b8d7-03f16964f185"), "Advanced analytics and reporting" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("1a2e9f6d-8dbb-4824-ac7b-61952ba98edc"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("39bc8015-ac4b-49ea-b38e-33cc93019f88"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("98eb2e10-1d7c-4836-9405-db267a95d5eb"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("a30d5cb6-00ff-4ad0-92ba-1319c00e75ca"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("a3181025-de65-46db-a34b-44648611c142"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("c1e369e0-e0e7-4c0b-9eed-9953bb3cbd53"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("c437f58d-36ef-4281-95d9-e44f05bd607c"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("f6bf6471-2ca2-46ad-aa8f-cb89c9c1beb0"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("0adac7ee-0ea8-4d37-8c7b-85405e9d469a"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("0ec8a3eb-4a7f-4381-b047-a1898febfaab"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("151c1c0c-8246-4df0-85ff-94afba2540c2"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("1e188ef8-554b-4787-88a8-e31612650171"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("1f81aa3b-0f18-4bb8-890e-9bcfed191d2d"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("20fe51bb-0542-44b7-80f1-7e344dcab02a"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("218e834a-b7fb-4d38-bfda-7ea4f692eb4d"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("28f77e3d-bcd1-4b79-b886-f9400d2c145f"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("4097affd-71b3-40ed-be5c-d6ccf52beb86"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("52b2d50d-89c8-4b7c-8c82-6fd0b58f22d9"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("5365c62b-80fe-4fd6-a33b-d2f0c37c14dd"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("6379b10c-a5dc-4b9d-a38f-46c0d3693a52"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("87fb7e30-3f38-462f-b22f-0748de25c575"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("90ce0366-5306-4146-8305-4c5b1b3318cc"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("98b87916-5815-401e-8954-40301b55c222"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("9c417a48-9b50-4143-a2e7-91ab0d56ce5c"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("a266a8d9-e116-479f-b3f4-d525ed90cdda"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("c13e915f-1c19-4337-89e6-8f2ca2073bc5"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("d3839e74-b362-43a6-92e3-73b403a368f5"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("f67bcc17-d3cd-440a-93e0-9d0782bda853"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("fe5a02e6-55da-4d21-9e7e-15c795b06545"));

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("0e30c8e6-ad20-4eea-a43d-09266dd018d8"));

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("612d8a00-e033-4023-964b-f030446ad156"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("3865574d-5903-47d3-843f-52fdae8ec179"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("41a26f05-d33f-49c3-b8d7-03f16964f185"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("58bf6843-e255-4236-a00f-e71ca119b139"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("70aff2c3-143b-4bc5-b292-1da9b533bab3"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("98479597-72fe-454f-9daa-d3f2c96fe5b2"));

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "Annually", "AnnualyPriceId", "Commission", "Discount", "ExternalId", "Level", "Metadata", "Monthly", "MonthlyPriceId", "Order", "PaymentMethod", "Tag", "Title" },
                values: new object[,]
                {
                    { new Guid("24a2fa48-6f7f-4eb1-affe-8b2d7a6c8d32"), 0m, "", 0.09m, 0m, "HMa_wvADlWQW", 1, null, 0m, "price_1OktQYC26XBdqsojcQwEpZTu", 2, "stripe", "", "Basic" },
                    { new Guid("bdcb831d-eeb6-4ff9-aebe-734fbbe40ce5"), 0m, "", 0m, 0m, "4eblv1PWMhI2", 2, null, 4.99m, "price_1OkwiHC26XBdqsojpgor0QaV", 3, "stripe", "Popular", "Professional" }
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "Group", "Value" },
                values: new object[,]
                {
                    { new Guid("5c3e48f4-9c88-4fd5-8aaf-b1193abc3243"), 2, "Which payment methods do you prefer for platform subscriptions and earnings withdrawals?" },
                    { new Guid("82125427-cf18-4654-81c7-371ec2c76a5d"), 3, "Do you prefer a monthly subscription fee or paying a commission on earnings?" },
                    { new Guid("9a5eec8c-e058-4d8f-9531-2ed736c0257c"), 4, "How useful do you find the WePromoLink platform for your advertising needs?" },
                    { new Guid("9f735dbe-e3a1-4d79-b95f-4499ac874a5e"), 5, "What additional features or improvements would you like to see on the WePromoLink platform?" },
                    { new Guid("bea9806c-3447-4de1-924f-fd9fd1dc79f2"), 1, "What motivates you to use a platform like WePromoLink?" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionFeatures",
                columns: new[] { "Id", "BoolValue", "Name", "SubscriptionPlanModelId", "Value" },
                values: new object[,]
                {
                    { new Guid("01cc70d3-d906-4082-93f3-634e8169fa5f"), false, "Contain ads", new Guid("bdcb831d-eeb6-4ff9-aebe-734fbbe40ce5"), "" },
                    { new Guid("1d9f30e3-428a-4dbe-ab17-980ff2ba99d1"), false, "Campaigns", new Guid("bdcb831d-eeb6-4ff9-aebe-734fbbe40ce5"), "Unlimited" },
                    { new Guid("2fb10b88-c042-4348-bd2e-ec6ca75f3645"), false, "Commission per click", new Guid("24a2fa48-6f7f-4eb1-affe-8b2d7a6c8d32"), "U$0.09" },
                    { new Guid("3612f2de-787b-4f67-987f-181d82ec7f86"), false, "Links", new Guid("bdcb831d-eeb6-4ff9-aebe-734fbbe40ce5"), "Unlimited" },
                    { new Guid("50061763-2048-418f-a10a-c6b24c7dd2e5"), false, "Commission per click", new Guid("bdcb831d-eeb6-4ff9-aebe-734fbbe40ce5"), "U$0.00" },
                    { new Guid("7b67f7b6-4d90-406b-8e37-724b23f572a3"), true, "Contain ads", new Guid("24a2fa48-6f7f-4eb1-affe-8b2d7a6c8d32"), "" },
                    { new Guid("b4deaea6-382b-44ee-bfdc-7fa5b1175932"), false, "Links", new Guid("24a2fa48-6f7f-4eb1-affe-8b2d7a6c8d32"), "Unlimited" },
                    { new Guid("c4ec5857-0583-4a64-8f24-4dc73397ec7b"), false, "Campaigns", new Guid("24a2fa48-6f7f-4eb1-affe-8b2d7a6c8d32"), "Unlimited" }
                });

            migrationBuilder.InsertData(
                table: "SurveyAnswers",
                columns: new[] { "Id", "SurveyQuestionModelId", "Value" },
                values: new object[,]
                {
                    { new Guid("00cb5ad4-db39-49bc-9086-56c964f08ddd"), new Guid("9a5eec8c-e058-4d8f-9531-2ed736c0257c"), "Somewhat useful" },
                    { new Guid("13efa766-abfd-43d3-8d8a-dd5c4b26bf7f"), new Guid("9a5eec8c-e058-4d8f-9531-2ed736c0257c"), "Not very useful" },
                    { new Guid("14e4045f-e49e-42ba-8626-6c8bd29a2ca1"), new Guid("82125427-cf18-4654-81c7-371ec2c76a5d"), "I'm not sure" },
                    { new Guid("1e80f4e9-0c9d-4762-880b-eab13f6ce7bf"), new Guid("5c3e48f4-9c88-4fd5-8aaf-b1193abc3243"), "Bank transfer" },
                    { new Guid("1f5975ab-d264-405e-9f84-6ac561bf2043"), new Guid("9f735dbe-e3a1-4d79-b95f-4499ac874a5e"), "Integration with other advertising platforms" },
                    { new Guid("3f25d65e-db90-4198-aaa0-0bb934ce7a2f"), new Guid("bea9806c-3447-4de1-924f-fd9fd1dc79f2"), "Earning money through affiliate marketing" },
                    { new Guid("4289cfa8-8fa9-439a-9112-e71b88f5f012"), new Guid("5c3e48f4-9c88-4fd5-8aaf-b1193abc3243"), "Credit/Debit card" },
                    { new Guid("43043f7a-84fb-4d64-81b4-d943f3eabbce"), new Guid("9f735dbe-e3a1-4d79-b95f-4499ac874a5e"), "Improved user interface and navigation" },
                    { new Guid("4e272e2d-5714-41ba-90c1-5c2c8744d0b9"), new Guid("bea9806c-3447-4de1-924f-fd9fd1dc79f2"), "Promoting my products or services" },
                    { new Guid("512a0c27-962b-4fe4-b2f3-f4ef2bd1ab2c"), new Guid("5c3e48f4-9c88-4fd5-8aaf-b1193abc3243"), "PayPal" },
                    { new Guid("5e36d09d-e7f2-4205-8b67-db0d345c5679"), new Guid("9f735dbe-e3a1-4d79-b95f-4499ac874a5e"), "I'm not sure" },
                    { new Guid("8f46e82d-cd5d-461c-a7ee-8da37ca66bb9"), new Guid("bea9806c-3447-4de1-924f-fd9fd1dc79f2"), "Connecting with other users and businesses" },
                    { new Guid("94005dee-685a-4357-9aa9-d7b708bf9126"), new Guid("82125427-cf18-4654-81c7-371ec2c76a5d"), "Commission on earnings" },
                    { new Guid("a080d796-9e78-4875-ac89-2ad515dd3273"), new Guid("9a5eec8c-e058-4d8f-9531-2ed736c0257c"), "Not useful at all" },
                    { new Guid("a5e9a232-8537-44c2-938f-cfda436efed7"), new Guid("9f735dbe-e3a1-4d79-b95f-4499ac874a5e"), "Advanced analytics and reporting" },
                    { new Guid("b1db5655-1315-4f86-b96c-70a2fe650af7"), new Guid("82125427-cf18-4654-81c7-371ec2c76a5d"), "Monthly subscription fee" },
                    { new Guid("bdc5cde1-4ff1-470e-9f8d-7895ea5efe1a"), new Guid("9a5eec8c-e058-4d8f-9531-2ed736c0257c"), "Extremely useful" },
                    { new Guid("bfca7e19-bfd9-4fc0-8052-48a358f4c6d0"), new Guid("bea9806c-3447-4de1-924f-fd9fd1dc79f2"), "Exploring new advertising opportunities" },
                    { new Guid("c8a44738-fb17-475b-983c-ff809fac2a1a"), new Guid("9a5eec8c-e058-4d8f-9531-2ed736c0257c"), "Very useful" },
                    { new Guid("cf7ba3b6-7a05-4334-b9ab-01cb65dae9e4"), new Guid("9f735dbe-e3a1-4d79-b95f-4499ac874a5e"), "More campaign customization options" },
                    { new Guid("e0e437dd-bab7-4aa3-bc38-771d87a0cc92"), new Guid("5c3e48f4-9c88-4fd5-8aaf-b1193abc3243"), "Stripe" }
                });
        }
    }
}
