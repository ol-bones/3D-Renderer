using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace 3DRenderer
{
    class Entity
    {
        protected Scene scene;
        public int ShaderID;
        public int ID;

        public Entity()
        {

        }

        public virtual void Init(Scene s)
        {
            this.scene = s;
            this.ID = s.Entities.Count;
        }
        
        public virtual void OnKeyUp(string k) { }
        public virtual void OnKeyDown(string k) { }

        public virtual void Delete()
        {
            this.scene = null;
            this.ShaderID = -1;
            this.ID = -1;
        }

        public virtual void Update(float gameTime)
        {

        }
    }
}
