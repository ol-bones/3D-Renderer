using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace 3DRenderer
{
    class Key
    {
        private string Name = "undefined";
        private bool keyDown = false;

        public Key(string name)
        {
            this.Name = name;
        }

        public string GetName()
        {
            return Name;
        }

        public bool KeyDown()
        {
            return keyDown;
        }

        public void keyPressed()
        {
            this.keyDown = true;
        }

        public void keyReleased()
        {
            this.keyDown = false;
        }
    }

    class EKeyboard
    {
        private List<Key> m_Keys;

        public EKeyboard()
        {
            this.CreateKeys();
        }

        private void CreateKeys()
        {
            this.m_Keys = new List<Key>();

            this.m_Keys.Add(new Key("W"));
            this.m_Keys.Add(new Key("A"));
            this.m_Keys.Add(new Key("S"));
            this.m_Keys.Add(new Key("D"));
            this.m_Keys.Add(new Key("Space"));
            this.m_Keys.Add(new Key("LAlt"));
            this.m_Keys.Add(new Key("Plus"));
        }

        public Key Key(string keyName)
        {
            try
            {
                foreach (Key k in this.m_Keys)
                {
                    if (k.GetName().Equals(keyName))
                    {
                        return k;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Keyboard error");
            }
            return null;
        }

        public void OnKeyDown(string k)
        {
            if (this.Key(k) == null) { return; }

            this.Key(k).keyPressed();
        }

        public void OnKeyUp(string k)
        {
            if (this.Key(k) == null) { return; }

            this.Key(k).keyReleased();
        }

    }
}
