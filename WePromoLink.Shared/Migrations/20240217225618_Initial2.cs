using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePromoLink.Shared.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("5c255353-0fcc-4c21-9945-490e3a40a385"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("a4f20b2f-de54-421d-ae16-63dc276b3223"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e702dab7-560f-46ea-a51c-522b401a4e39"));

            migrationBuilder.DeleteData(
                table: "SubscriptionFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e73e19b1-98a7-4d0b-bb00-d6e5273d4232"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("009a2738-b1f0-4a05-9900-5a1925cbf42b"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("11cf7250-05fe-466e-883f-29959823808a"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("1bef408c-1c69-416b-9e87-86aaa175ee60"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("2f413356-2958-4594-a084-c18fa01bc80a"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("2f46f69a-b0db-45c1-afe7-0cec7e772dc8"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("33aefef1-1601-42cb-a18a-3059fdcc782d"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("3a162e33-11e0-401d-8bff-5f37dd5c10f5"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("65593b8c-3d84-47af-81fa-2591b5ae9f2f"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("83f7e6e0-daf5-4c19-93da-b8fb50e4935c"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("8418c289-686a-46a9-bb27-84ca5249637e"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("86dab0a0-2212-4cee-8913-39a8b7bdbba2"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("8aee4bc3-db2d-4ecd-b474-0d26a6d21d6d"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("92d229d7-2b00-4eeb-b287-920efc5babe0"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("a87ae105-22d1-40bf-8059-ea39b73009bc"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("ba66e195-63a1-47cc-8f68-276f9414146a"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("ccc5973b-1ce3-43fa-bd37-ec35bc64c312"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("d969b536-753c-4ae4-9389-68c6879f55b5"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("de2b7e59-276d-47e3-a6ab-6d39592ded8c"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("e15eb85f-6ec1-4816-9c5e-e60dfd6524fe"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("e47c1e8d-d4bb-4f44-9f72-d0a4c1fbaaf4"));

            migrationBuilder.DeleteData(
                table: "SurveyAnswers",
                keyColumn: "Id",
                keyValue: new Guid("e7f21638-23c4-4ad0-8a1d-da61670e5948"));

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: new Guid("4c440ccb-4797-481c-a428-5893c5493aad"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("191c668b-e3e4-4169-bb51-f3e6e740d86b"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("943590a1-6d2f-4d2e-be37-7a3d06f42b83"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("eae5653d-be97-4674-81eb-5dde7d2fc7ef"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("4c440ccb-4797-481c-a428-5893c5493aad"), 0m, "", 0.09m, 0m, "il7Tbn3V4iFw", 1, null, 0m, "price_1OktQYC26XBdqsojcQwEpZTu", 2, "stripe", "Popular", "Basic" });

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "Group", "Value" },
                values: new object[,]
                {
                    { new Guid("191c668b-e3e4-4169-bb51-f3e6e740d86b"), 2, "Which payment methods do you prefer for platform subscriptions and earnings withdrawals?" },
                    { new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"), 5, "What additional features or improvements would you like to see on the WePromoLink platform?" },
                    { new Guid("943590a1-6d2f-4d2e-be37-7a3d06f42b83"), 3, "Do you prefer a monthly subscription fee or paying a commission on earnings?" },
                    { new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"), 4, "How useful do you find the WePromoLink platform for your advertising needs?" },
                    { new Guid("eae5653d-be97-4674-81eb-5dde7d2fc7ef"), 1, "What motivates you to use a platform like WePromoLink?" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionFeatures",
                columns: new[] { "Id", "BoolValue", "Name", "SubscriptionPlanModelId", "Value" },
                values: new object[,]
                {
                    { new Guid("5c255353-0fcc-4c21-9945-490e3a40a385"), false, "Links", new Guid("4c440ccb-4797-481c-a428-5893c5493aad"), "Unlimited" },
                    { new Guid("a4f20b2f-de54-421d-ae16-63dc276b3223"), true, "Contain ads", new Guid("4c440ccb-4797-481c-a428-5893c5493aad"), "" },
                    { new Guid("e702dab7-560f-46ea-a51c-522b401a4e39"), false, "Commission per click", new Guid("4c440ccb-4797-481c-a428-5893c5493aad"), "U$0.09" },
                    { new Guid("e73e19b1-98a7-4d0b-bb00-d6e5273d4232"), false, "Campaigns", new Guid("4c440ccb-4797-481c-a428-5893c5493aad"), "Unlimited" }
                });

            migrationBuilder.InsertData(
                table: "SurveyAnswers",
                columns: new[] { "Id", "SurveyQuestionModelId", "Value" },
                values: new object[,]
                {
                    { new Guid("009a2738-b1f0-4a05-9900-5a1925cbf42b"), new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"), "Extremely useful" },
                    { new Guid("11cf7250-05fe-466e-883f-29959823808a"), new Guid("191c668b-e3e4-4169-bb51-f3e6e740d86b"), "Bank transfer" },
                    { new Guid("1bef408c-1c69-416b-9e87-86aaa175ee60"), new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"), "Not useful at all" },
                    { new Guid("2f413356-2958-4594-a084-c18fa01bc80a"), new Guid("eae5653d-be97-4674-81eb-5dde7d2fc7ef"), "Connecting with other users and businesses" },
                    { new Guid("2f46f69a-b0db-45c1-afe7-0cec7e772dc8"), new Guid("943590a1-6d2f-4d2e-be37-7a3d06f42b83"), "Monthly subscription fee" },
                    { new Guid("33aefef1-1601-42cb-a18a-3059fdcc782d"), new Guid("eae5653d-be97-4674-81eb-5dde7d2fc7ef"), "Promoting my products or services" },
                    { new Guid("3a162e33-11e0-401d-8bff-5f37dd5c10f5"), new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"), "Not very useful" },
                    { new Guid("65593b8c-3d84-47af-81fa-2591b5ae9f2f"), new Guid("eae5653d-be97-4674-81eb-5dde7d2fc7ef"), "Earning money through affiliate marketing" },
                    { new Guid("83f7e6e0-daf5-4c19-93da-b8fb50e4935c"), new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"), "Somewhat useful" },
                    { new Guid("8418c289-686a-46a9-bb27-84ca5249637e"), new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"), "I'm not sure" },
                    { new Guid("86dab0a0-2212-4cee-8913-39a8b7bdbba2"), new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"), "Very useful" },
                    { new Guid("8aee4bc3-db2d-4ecd-b474-0d26a6d21d6d"), new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"), "More campaign customization options" },
                    { new Guid("92d229d7-2b00-4eeb-b287-920efc5babe0"), new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"), "Integration with other advertising platforms" },
                    { new Guid("a87ae105-22d1-40bf-8059-ea39b73009bc"), new Guid("943590a1-6d2f-4d2e-be37-7a3d06f42b83"), "Commission on earnings" },
                    { new Guid("ba66e195-63a1-47cc-8f68-276f9414146a"), new Guid("943590a1-6d2f-4d2e-be37-7a3d06f42b83"), "I'm not sure" },
                    { new Guid("ccc5973b-1ce3-43fa-bd37-ec35bc64c312"), new Guid("191c668b-e3e4-4169-bb51-f3e6e740d86b"), "Stripe" },
                    { new Guid("d969b536-753c-4ae4-9389-68c6879f55b5"), new Guid("191c668b-e3e4-4169-bb51-f3e6e740d86b"), "PayPal" },
                    { new Guid("de2b7e59-276d-47e3-a6ab-6d39592ded8c"), new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"), "Advanced analytics and reporting" },
                    { new Guid("e15eb85f-6ec1-4816-9c5e-e60dfd6524fe"), new Guid("eae5653d-be97-4674-81eb-5dde7d2fc7ef"), "Exploring new advertising opportunities" },
                    { new Guid("e47c1e8d-d4bb-4f44-9f72-d0a4c1fbaaf4"), new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"), "Improved user interface and navigation" },
                    { new Guid("e7f21638-23c4-4ad0-8a1d-da61670e5948"), new Guid("191c668b-e3e4-4169-bb51-f3e6e740d86b"), "Credit/Debit card" }
                });
        }
    }
}
