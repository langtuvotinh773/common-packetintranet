using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace PacketIntranet.Helpers
{
    public class GmailSender
    {
        public string gMailAccount { get; set; }
        public string password { get; set; }
        public string from { get; set; }
        public string from_name { get; set; }
        public string to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public int status { get; set; }


        // status : 0 : chua nhan gui
        //          1 : Gui Thanh cong
        //          2 : Gui that bai

        public string error { get; set; }
        public GmailSender()
        {
            this.subject = "";
            this.content = "";
            this.bcc = "";
            this.cc = "";
            this.password = "";
            this.gMailAccount = "";
            this.status = 0;
            this.error = "";
            this.from = "";
            this.from_name = "";
        }
        public int SendMail()
        {
            try
            {
                NetworkCredential loginInfo = new NetworkCredential(this.gMailAccount, this.password);
                MailMessage msg = new MailMessage();
                string s = this.from;
                msg.From = new MailAddress(this.from, this.from_name);

                List<MailAddress> mailTo = GmailSender.split_mail(this.to, ',');
                foreach (MailAddress mail in mailTo)
                {
                    msg.To.Add(mail);
                }

                List<MailAddress> mailCc = GmailSender.split_mail(this.cc, ',');
                foreach (MailAddress mail in mailCc)
                {
                    msg.CC.Add(mail);
                }

                List<MailAddress> mailBcc = GmailSender.split_mail(this.bcc, ',');
                foreach (MailAddress mail in mailBcc)
                {
                    msg.Bcc.Add(mail);
                }

                msg.Subject = this.subject;
                msg.Body = this.content;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = loginInfo;
                client.Send(msg);

                this.status = 1;
                return status;
            }
            catch (Exception ex)
            {
                this.status = 2;
                this.error = ex.Message.ToString();
                return status;
            }
        }

        private static List<MailAddress> split_mail(string strmail, char strchar)
        {
            List<MailAddress> lm = new List<MailAddress>();
            string[] mails = strmail.Split(strchar);
            foreach (string mail in mails)
            {
                if (GmailSender.isEmail(mail.Trim()))
                    lm.Add(new MailAddress(mail));
            }
            return lm;
        }

        private static bool isEmail(string inputEmail)
        {
            try
            {
                System.Net.Mail.MailAddress mail = new MailAddress(inputEmail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}