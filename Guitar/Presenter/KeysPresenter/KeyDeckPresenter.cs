using Guitar.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guitar.Models.KeysModel;
using System.Reflection;
using System.Windows.Forms;
using Guitar.Models;

namespace Guitar.Presenter
{
    internal class KeyDeckPresenter
    {
        private readonly Type ourtype = typeof(AbstractKey); // Базовый тип
        private readonly IEnumerable<Type> listKeyDecks;
        private readonly IKeysEvent keysEvent;
        private readonly StateGuitar stateGuitar;

        public KeyDeckPresenter(IKeysEvent keysEvent, StateGuitar stateGuitar)
        {
            listKeyDecks = Assembly.GetAssembly(ourtype).GetTypes().Where(type => type.IsSubclassOf(ourtype));  // using System.Linq
            this.keysEvent = keysEvent;
            keysEvent.KDown += KeysEvent_KDown;
            keysEvent.KUp += KeysEvent_KUp;
            keysEvent.KPress += KeysEvent_KPress;
            this.stateGuitar = stateGuitar;
        }

        private void KeysEvent_KPress(object sender, KeyPressEventArgs e)
        {
            //   MessageBox.Show("");
        }

        private void KeysEvent_KUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            foreach (Type itm in listKeyDecks)
            {
                object obj = Activator.CreateInstance(itm);
                if (e.KeyCode.ToString() == (itm.GetProperty("Key").GetValue(obj).ToString()))
                {
                    itm.GetMethod("EventKey").Invoke(obj, new object[] { stateGuitar.StateButtonDecks, false });
                }
            }
        }

        private void KeysEvent_KDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            foreach (Type itm in listKeyDecks)
            {
                object obj = Activator.CreateInstance(itm);
                if (e.KeyCode.ToString() == (itm.GetProperty("Key").GetValue(obj).ToString()))
                {
                    itm.GetMethod("EventKey").Invoke(obj, new object[] { stateGuitar.StateButtonDecks, true });
                }
            }
        }
    }
}