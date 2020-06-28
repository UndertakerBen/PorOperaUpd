using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portable_Opera_Updater
{
    public partial class Langfile
    {
        public static string Texts(string langText)
        {
            CultureInfo culture1 = CultureInfo.CurrentUICulture;
            switch (culture1.TwoLetterISOLanguageName)
            {
                case "ru":
                    switch (langText)
                    {
                        case "Button10":
                            return "Выход";
                        case "Button9":
                            return "Установить все";
                        case "Button9UAll":
                            return "Обновить все";
                        case "Label9":
                            return "Установить все версии x86 и/или x64";
                        case "checkBox4":
                            return "Игнорировать проверку версии";
                        case "checkBox3":
                            return "Разные версии в отдельных папках";
                        case "checkBox5":
                            return "Создать ярлык на рабочем столе";
                        case "downUnpstart":
                            return "Распаковка";
                        case "downUnpfine":
                            return "Распакованный";
                        case "infoLabel":
                            return "Доступна новая версия";
                        case "laterButton":
                            return "нет";
                        case "updateButton":
                            return "Да";
                        case "downLabel":
                            return "ОБНОВИТЬ";
                        case "MeassageVersion":
                            return "Данная версия уже установлена";
                        case "MeassageRunning":
                            return "Необходимо закрыть Opera перед обновлением.";
                        case "Register":
                            return "регистр";
                        case "Remove":
                            return "Удалить";
                        case "Standard":
                            return " как браузер по умолчанию";
                        case "Extra":
                            return "отде́льно";
                        case "VInfo":
                            return "О версиях";
                    }
                    break;
                case "de":
                    switch (langText)
                    {
                        case "Button10":
                            return "Beenden";
                        case "Button9":
                            return "Alle Installieren";
                        case "Button9UAll":
                            return "Alle Updaten";
                        case "Label9":
                            return "Alle x86 und oder x64 installieren";
                        case "checkBox4":
                            return "Versionkontrolle ignorieren";
                        case "checkBox3":
                            return "Für jede Version einen eigenen Ordner";
                        case "checkBox5":
                            return "Eine Verknüpfung auf dem Desktop erstellen";
                        case "downUnpstart":
                            return "Entpacken";
                        case "downUnpfine":
                            return "Entpackt";
                        case "infoLabel":
                            return "Eine neue Version ist verfügbar";
                        case "laterButton":
                            return "Nein";
                        case "updateButton":
                            return "Ja";
                        case "downLabel":
                            return "Jetzt Updaten";
                        case "MeassageVersion":
                            return "Die selbe Version ist bereits installiert";
                        case "MeassageRunning":
                            return "Bitte schließen Sie den laufenden Opera, bevor Sie den Browser aktualisieren.";
                        case "Register":
                            return "Registrieren";
                        case "Remove":
                            return "Entfernen";
                        case "Standard":
                            return " als Standardbrowser";
                        case "Extra":
                            return "Extras";
                        case "VInfo":
                            return "Versions Info";
                    }
                    break;
                default:
                    switch (langText)
                    {
                        case "Button10":
                            return "Quit";
                        case "Button9":
                            return "Install all";
                        case "Button9UAll":
                            return "Update all";
                        case "Label9":
                            return "Install all x86 and or x64";
                        case "checkBox4":
                            return "Ignore version check";
                        case "checkBox3":
                            return "Create a Folder for each version";
                        case "checkBox5":
                            return "Create a shortcut on the desktop";
                        case "downUnpstart":
                            return "Unpacking";
                        case "downUnpfine":
                            return "Unpacked";
                        case "infoLabel":
                            return "A new version is available";
                        case "laterButton":
                            return "No";
                        case "updateButton":
                            return "Yes";
                        case "downLabel":
                            return "Update now";
                        case "MeassageVersion":
                            return "The same version is already installed";
                        case "MeassageRunning":
                            return "Please close the running Opera before updating the browser.";
                        case "Register":
                            return "Register";
                        case "Remove":
                            return "Remove";
                        case "Standard":
                            return " as default browser";
                        case "Extra":
                            return "Extras";
                        case "VInfo":
                            return "Version Info";
                    }
                    break;
            }
            return "";
        }
    }
}
