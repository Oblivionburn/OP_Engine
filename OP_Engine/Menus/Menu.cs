using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Controls;
using OP_Engine.Utility;

namespace OP_Engine.Menus
{
    public class Menu : Something
    {
        #region Variables

        public int Current_Button;
        public bool Active;

        public List<Button> Buttons = new List<Button>();
        public List<Picture> Pictures = new List<Picture>();
        public List<Label> Labels = new List<Label>();
        public List<Slider> Sliders = new List<Slider>();
        public List<InputBox> Inputs = new List<InputBox>();
        public List<ProgressBar> ProgressBars = new List<ProgressBar>();
        public Color nullColor = new Color(0, 0, 0, 0);

        #endregion

        #region Constructors

        public Menu()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            if (Visible ||
                Active)
            {
                foreach (Button button in Buttons)
                {
                    button.Update();
                }

                foreach (Label label in Labels)
                {
                    label.Update();
                }

                foreach (InputBox input in Inputs)
                {
                    input.Update();
                }

                foreach (ProgressBar bar in ProgressBars)
                {
                    bar.Update();
                }
            }
        }

        public virtual void Update(Game gameRef, ContentManager content)
        {
            if (Visible ||
                Active)
            {
                foreach (Button button in Buttons)
                {
                    button.Update();
                }

                foreach (Label label in Labels)
                {
                    label.Update();
                }

                foreach (InputBox input in Inputs)
                {
                    input.Update();
                }

                foreach (ProgressBar bar in ProgressBars)
                {
                    bar.Update();
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                foreach (Picture existing in Pictures)
                {
                    existing.Draw(spriteBatch);
                }

                foreach (Button existing in Buttons)
                {
                    existing.Draw(spriteBatch);
                }

                foreach (Slider existing in Sliders)
                {
                    existing.Draw(spriteBatch, existing.DrawColor);
                }

                foreach (InputBox existing in Inputs)
                {
                    existing.Draw(spriteBatch);
                }

                foreach (ProgressBar existing in ProgressBars)
                {
                    existing.Draw(spriteBatch);
                }

                foreach (Label existing in Labels)
                {
                    existing.Draw(spriteBatch);
                }
            }
        }

        public virtual void Load()
        {

        }

        public virtual void Load(ContentManager content)
        {

        }

        public virtual void Resize(Point point)
        {

        }

        public virtual void AddButton(long id, string name, Texture2D Texture, Texture2D texture_hover, Texture2D texture_disabled, Rectangle region, Color color, bool visible)
        {
            Button button = new Button();
            button.ID = id;
            button.Name = name;
            button.Texture = Texture;
            button.Texture_Highlight = texture_hover;
            button.Texture_Disabled = texture_disabled;
            button.Region = new Rectangle(region.X, region.Y, region.Width, region.Height);
            button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            button.DrawColor = color;
            button.Visible = visible;
            button.Enabled = true;
            Buttons.Add(button);
        }

        public virtual void AddButton(SpriteFont font, long id, string name, string text, Color text_color, Color text_highlight_color, Texture2D texture, Rectangle region, bool selected, bool visible)
        {
            Button button = new Button();
            button.ID = id;
            button.Name = name;
            button.Text = text;
            button.Font = font;
            button.TextColor = text_color;
            button.TextColor_Selected = text_highlight_color;
            button.DrawColor = Color.White;
            button.Texture = texture;
            button.Region = new Rectangle(region.X, region.Y, region.Width, region.Height);
            button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            button.Selected = selected;
            button.Visible = visible;
            Buttons.Add(button);
        }

        public virtual void AddButton(SpriteFont font, long id, string name, string text, Color text_color, Color text_highlight_color, Texture2D texture, Rectangle region, Color draw_color, bool selected, bool visible)
        {
            Button button = new Button();
            button.ID = id;
            button.Name = name;
            button.Text = text;
            button.Font = font;
            button.TextColor = text_color;
            button.TextColor_Selected = text_highlight_color;
            button.DrawColor = draw_color;
            button.Texture = texture;
            button.Region = new Rectangle(region.X, region.Y, region.Width, region.Height);
            button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            button.Selected = selected;
            button.Visible = visible;
            Buttons.Add(button);
        }

        public virtual void AddButton(SpriteFont font, long id, string name, string text, Color text_color, Color text_highlight_color, Color text_disabled_color, Texture2D texture, Rectangle region, bool selected, bool visible)
        {
            Button button = new Button();
            button.ID = id;
            button.Name = name;
            button.Text = text;
            button.Font = font;
            button.TextColor = text_color;
            button.TextColor_Selected = text_highlight_color;
            button.TextColor_Disabled = text_disabled_color;
            button.DrawColor = Color.White;
            button.Texture = texture;
            button.Region = new Rectangle(region.X, region.Y, region.Width, region.Height);
            button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            button.Selected = selected;
            button.Visible = visible;
            Buttons.Add(button);
        }

        public virtual void AddButton(SpriteFont font, long id, string name, string text, Color text_color, Color text_highlight_color, Color text_disabled_color, Texture2D texture, Rectangle region, Color draw_color, bool selected, bool visible)
        {
            Button button = new Button();
            button.ID = id;
            button.Name = name;
            button.Text = text;
            button.Font = font;
            button.TextColor = text_color;
            button.TextColor_Selected = text_highlight_color;
            button.TextColor_Disabled = text_disabled_color;
            button.DrawColor = draw_color;
            button.Texture = texture;
            button.Region = new Rectangle(region.X, region.Y, region.Width, region.Height);
            button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            button.Selected = selected;
            button.Visible = visible;
            Buttons.Add(button);
        }

        public virtual void AddPicture(long id, string name, Texture2D texture, Rectangle region, Color color, bool visible)
        {
            Picture picture = new Picture();
            picture.ID = id;
            picture.Name = name;
            picture.Texture = texture;
            picture.Region = new Rectangle(region.X, region.Y, region.Width, region.Height);
            picture.Image = new Rectangle(0, 0, picture.Texture.Width, picture.Texture.Height);
            picture.DrawColor = color;
            picture.Visible = visible;
            Pictures.Add(picture);
        }

        public virtual void AddLabel(SpriteFont font, long id, string name, string text, Color text_color, Rectangle region, bool visible)
        {
            Label label = new Label();
            label.ID = id;
            label.Name = name;
            label.Value = 255;
            label.Text = text;
            label.TextColor = text_color;
            label.Font = font;
            label.Region = region;
            label.Visible = visible;
            Labels.Add(label);
        }

        public virtual void AddLabel(SpriteFont font, long id, string name, string text, Color text_color, Texture2D texture, Rectangle region, bool visible)
        {
            Label label = new Label();
            label.ID = id;
            label.Name = name;
            label.Text = text;
            label.Font = font;
            label.TextColor = text_color;
            label.DrawColor = Color.White;
            label.Texture = texture;
            label.Region = region;
            label.Image = new Rectangle(0, 0, label.Texture.Width, label.Texture.Height);
            label.Visible = visible;
            Labels.Add(label);
        }

        public virtual void AddLabel(SpriteFont font, long id, string name, string text, Color text_color, Texture2D texture, Rectangle region, Color draw_color, bool visible)
        {
            Label label = new Label();
            label.ID = id;
            label.Name = name;
            label.Text = text;
            label.Font = font;
            label.TextColor = text_color;
            label.DrawColor = draw_color;
            label.Texture = texture;
            label.Region = region;
            label.Image = new Rectangle(0, 0, label.Texture.Width, label.Texture.Height);
            label.Visible = visible;
            Labels.Add(label);
        }

        public virtual void AddProgressBar(long id, string name, int max, int value, int increment, Texture2D base_texture, Texture2D bar_texture, Rectangle region, Color color, bool visible)
        {
            ProgressBar progressBar = new ProgressBar();
            progressBar.ID = id;
            progressBar.Name = name;
            progressBar.Value = value;
            progressBar.Rate = increment;
            progressBar.Max_Value = max;
            progressBar.Base_Region = new Rectangle(region.X, region.Y, region.Width, region.Height);
            progressBar.Bar_Region = new Rectangle(region.X, region.Y, 0, region.Height);
            progressBar.Base_Texture = base_texture;
            progressBar.Bar_Texture = bar_texture;
            progressBar.Bar_Image = new Rectangle(0, 0, 0, progressBar.Base_Texture.Height);
            progressBar.DrawColor = color;
            progressBar.Visible = visible;

            float num = progressBar.Bar_Texture.Width / 100f * progressBar.Value;
            progressBar.Bar_Image = new Rectangle(progressBar.Bar_Image.X, progressBar.Bar_Image.Y, (int)num, progressBar.Bar_Image.Height);
            num = progressBar.Base_Region.Width / 100f * progressBar.Value;
            progressBar.Bar_Region = new Rectangle(progressBar.Base_Region.X, progressBar.Base_Region.Y, (int)num, progressBar.Base_Region.Height);

            ProgressBars.Add(progressBar);
        }

        public virtual void AddInput(SpriteFont font, long id, int max_length, string name, string text, Color text_color, Texture2D texture, Rectangle region, bool active, bool visible)
        {
            InputBox input = new InputBox();
            input.ID = id;
            input.MaxLength = max_length;
            input.Name = name;
            input.Text = text;
            input.Font = font;
            input.TextColor = text_color;
            input.DrawColor = Color.White;
            input.Texture = texture;
            input.Region = new Rectangle(region.X, region.Y, region.Width, region.Height);
            input.Image = new Rectangle(0, 0, input.Texture.Width, input.Texture.Height);
            input.Visible = visible;
            input.Opacity = 0.8f;
            input.Active = active;
            Inputs.Add(input);
        }

        public virtual Button GetButton(string name)
        {
            foreach (Button existing in Buttons)
            {
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual Button GetButton(long id)
        {
            foreach (Button existing in Buttons)
            {
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual Button GetButton_Current()
        {
            foreach (Button existing in Buttons)
            {
                if (existing.ID == Current_Button)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual Label GetLabel(string name)
        {
            foreach (Label existing in Labels)
            {
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual Label GetLabel(long id)
        {
            foreach (Label existing in Labels)
            {
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual Picture GetPicture(string name)
        {
            foreach (Picture existing in Pictures)
            {
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual Picture GetPicture(long id)
        {
            foreach (Picture existing in Pictures)
            {
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual Slider GetSlider(string name)
        {
            foreach (Slider existing in Sliders)
            {
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual Slider GetSlider(long id)
        {
            foreach (Slider existing in Sliders)
            {
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual InputBox GetInput(string name)
        {
            foreach (InputBox existing in Inputs)
            {
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual InputBox GetInput(long id)
        {
            foreach (InputBox existing in Inputs)
            {
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual ProgressBar GetProgressBar(string name)
        {
            foreach (ProgressBar existing in ProgressBars)
            {
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual ProgressBar GetProgressBar(long id)
        {
            foreach (ProgressBar existing in ProgressBars)
            {
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual void Clear()
        {
            foreach (Button button in Buttons)
            {
                button.Dispose();
            }
            Buttons.Clear();

            foreach (Picture picture in Pictures)
            {
                picture.Dispose();
            }
            Pictures.Clear();

            foreach (Label label in Labels)
            {
                label.Dispose();
            }
            Labels.Clear();

            foreach (Slider slider in Sliders)
            {
                slider.Dispose();
            }
            Sliders.Clear();

            foreach (InputBox input in Inputs)
            {
                input.Dispose();
            }
            Inputs.Clear();

            foreach (ProgressBar bar in ProgressBars)
            {
                bar.Dispose();
            }
            ProgressBars.Clear();
        }

        public override void Dispose()
        {
            foreach (Button button in Buttons)
            {
                button.Dispose();
            }

            foreach (Picture picture in Pictures)
            {
                picture.Dispose();
            }

            foreach (Label label in Labels)
            {
                label.Dispose();
            }

            foreach (Slider slider in Sliders)
            {
                slider.Dispose();
            }

            foreach (InputBox input in Inputs)
            {
                input.Dispose();
            }

            foreach (ProgressBar bar in ProgressBars)
            {
                bar.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
