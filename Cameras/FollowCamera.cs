using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace 3DRenderer
{
    class FollowCamera : Camera
    {
        PhysicalEntity Object;
        Vector3 Offset;

        public Key KEY_PLUS;
        public FollowCamera(ref Vector3 Pos, ref EKeyboard mKeyboard, ref ShaderUtility mShader)
            : base(ref Pos, ref mKeyboard, ref mShader)
        {

        }

        public override void Init(Scene s)
        {
            base.Init(s);
            this.Offset = new Vector3(0.0f, 0.0f, 1.0f);
            Follow(s.findEntityByID(1) as PhysicalEntity);

            this.KEY_PLUS = this.r_mKeyboard.Key("Plus");
        }

        public bool Follow(PhysicalEntity e)
        {
            if (e != null && e.GetType().IsSubclassOf(typeof(PhysicalEntity)))
            {
                this.Object = e;
                return true;
            }
            return false;
        }

        private void NextObject()
        {
            int i = (this.Object.ID + 1 > this.scene.Entities.Count) ? 1 : this.Object.ID + 1;
            while (!Follow((this.scene.findEntityByID(i)) as PhysicalEntity))
            {
                i = (i + 1 > this.scene.Entities.Count) ? 1 : i + 1;
            }
        }

        private void PreviousObject()
        {
            int i = (this.Object.ID - 1 <= 0) ? this.scene.Entities.Count - 1 : this.Object.ID - 1;
            while (!Follow((this.scene.findEntityByID(i)) as PhysicalEntity))
            {
                i = (i - 1 <= 0) ? this.scene.Entities.Count - 1 : i - 1;
            }
        }

        public override void OnKeyDown(string k)
        {
 	        base.OnKeyUp(k);
            if (k == "Plus")
            {
                NextObject();
            }
            else if (k == "Minus")
            {
                PreviousObject();
            }
        }
        public override void Update(float gameTime)
        {
            if (this.isActive())
            {
                if (this.Object == null || this.Object.ID == -1)
                {
                    NextObject();
                }
                base.Update(gameTime);
                Vector3 L = -this.Object.Position - this.Position;
                if (L.Length >= 0.01f)
                {
                    this.Position += ((L).Normalized() / (100 - this.MoveSpeed * gameTime));
                }
                this.mView = Matrix4.CreateTranslation(this.Position - this.Offset);
            }
        }
    }
}
