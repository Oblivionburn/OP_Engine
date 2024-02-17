using System;
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

        public List<Button> Buttons = new List<Button>();
        public List<Picture> Pictures = new List<Picture>();
        public List<Label> Labels = new List<Label>();
        public List<Slider> Sliders = new List<Slider>();
        public List<InputBox> Inputs = new List<InputBox>();
        public List<ProgressBar> ProgressBars = new List<ProgressBar>();
        public Color nullColor = new Color(0, 0, 0, 0);

        #endregion

        #region Events

        public event EventHandler OnOpen;
        public event EventHandler OnClose;
        public event EventHandler OnLoad;
        public event EventHandler OnClear;
        public event EventHandler OnAddButton;
        public event EventHandler OnAddPicture;
        public event EventHandler OnAddLabel;
        public event EventHandler OnAddProgressBar;
        public event EventHandler OnAddInput;

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

        public virtual void Open()
        {
            OnOpen?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Close()
        {
            OnClose?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Load()
        {
            OnLoad?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Clear()
        {
            Buttons.Clear();
            Pictures.Clear();
            Labels.Clear();
            Sliders.Clear();
            Inputs.Clear();
            ProgressBars.Clear();

            OnClear?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Load(ContentManager content)
        {
            OnLoad?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Resize(Point point)
        {

        }

        public void AddButton(long id, string name, Texture2D texture, Texture2D texture_hover, Texture2D texture_disabled, Region region, Color color, bool visible)
        {
            Button button = new Button();
            button.ID = id;
            button.Name = name;
            button.Texture = texture;
            button.Texture_Highlight = texture_hover;
            button.Texture_Disabled = texture_disabled;
            button.Region = region;

            if (button.Texture != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            }
            else if (button.Texture_Highlight != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture_Highlight.Width, button.Texture_Highlight.Height);
            }
            else if (button.Texture_Disabled != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture_Disabled.Width, button.Texture_Disabled.Height);
            }

            button.DrawColor = color;
            button.Visible = visible;
            button.Enabled = true;
            Buttons.Add(button);

            OnAddButton?.Invoke(this, EventArgs.Empty);
        }

        public void AddButton(SpriteFont font, long id, string name, string text, Color text_color, Color text_highlight_color, Texture2D texture, Region region, bool selected, bool visible)
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
            button.Region = region;
            button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            button.Selected = selected;
            button.Visible = visible;
            Buttons.Add(button);

            OnAddButton?.Invoke(this, EventArgs.Empty);
        }

        public void AddButton(SpriteFont font, long id, string name, string text, Color text_color, Color text_highlight_color, Texture2D texture, Region region, Color draw_color, bool selected, bool visible)
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
            button.Region = region;
            button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            button.Selected = selected;
            button.Visible = visible;
            Buttons.Add(button);

            OnAddButton?.Invoke(this, EventArgs.Empty);
        }

        public void AddButton(SpriteFont font, long id, string name, string text, Color text_color, Color text_highlight_color, Color text_disabled_color, Texture2D texture, Region region, bool selected, bool visible)
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
            button.Region = region;
            button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            button.Selected = selected;
            button.Visible = visible;
            Buttons.Add(button);

            OnAddButton?.Invoke(this, EventArgs.Empty);
        }

        public void AddButton(SpriteFont font, long id, string name, string text, Color text_color, Color text_highlight_color, Color text_disabled_color, Texture2D texture, Region region, Color draw_color, bool selected, bool visible)
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
            button.Region = region;
            button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            button.Selected = selected;
            button.Visible = visible;
            Buttons.Add(button);

            OnAddButton?.Invoke(this, EventArgs.Empty);
        }

        public void AddButton(SpriteFont font, long id, string name, string text, Color text_color, Color text_highlight_color, Texture2D texture, Texture2D texture_hover, Texture2D texture_disabled, Region region, bool selected, bool visible)
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
            button.Texture_Highlight = texture_hover;
            button.Texture_Disabled = texture_disabled;
            button.Region = region;

            if (button.Texture != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            }
            else if (button.Texture_Highlight != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture_Highlight.Width, button.Texture_Highlight.Height);
            }
            else if (button.Texture_Disabled != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture_Disabled.Width, button.Texture_Disabled.Height);
            }

            button.Selected = selected;
            button.Visible = visible;
            Buttons.Add(button);

            OnAddButton?.Invoke(this, EventArgs.Empty);
        }

        public void AddButton(SpriteFont font, long id, string name, string text, Color text_color, Color text_highlight_color, Texture2D texture, Texture2D texture_hover, Texture2D texture_disabled, Region region, Color draw_color, bool selected, bool visible)
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
            button.Texture_Highlight = texture_hover;
            button.Texture_Disabled = texture_disabled;
            button.Region = region;

            if (button.Texture != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            }
            else if (button.Texture_Highlight != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture_Highlight.Width, button.Texture_Highlight.Height);
            }
            else if (button.Texture_Disabled != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture_Disabled.Width, button.Texture_Disabled.Height);
            }

            button.Selected = selected;
            button.Visible = visible;
            Buttons.Add(button);

            OnAddButton?.Invoke(this, EventArgs.Empty);
        }

        public void AddButton(SpriteFont font, long id, string name, string text, Color text_color, Color text_highlight_color, Color text_disabled_color, Texture2D texture, Texture2D texture_hover, Texture2D texture_disabled, Region region, bool selected, bool visible)
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
            button.Texture_Highlight = texture_hover;
            button.Texture_Disabled = texture_disabled;
            button.Region = region;

            if (button.Texture != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            }
            else if (button.Texture_Highlight != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture_Highlight.Width, button.Texture_Highlight.Height);
            }
            else if (button.Texture_Disabled != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture_Disabled.Width, button.Texture_Disabled.Height);
            }

            button.Selected = selected;
            button.Visible = visible;
            Buttons.Add(button);

            OnAddButton?.Invoke(this, EventArgs.Empty);
        }

        public void AddButton(SpriteFont font, long id, string name, string text, Color text_color, Color text_highlight_color, Color text_disabled_color, Texture2D texture, Texture2D texture_hover, Texture2D texture_disabled, Region region, Color draw_color, bool selected, bool visible)
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
            button.Texture_Highlight = texture_hover;
            button.Texture_Disabled = texture_disabled;
            button.Region = region;

            if (button.Texture != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture.Width, button.Texture.Height);
            }
            else if (button.Texture_Highlight != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture_Highlight.Width, button.Texture_Highlight.Height);
            }
            else if (button.Texture_Disabled != null)
            {
                button.Image = new Rectangle(0, 0, button.Texture_Disabled.Width, button.Texture_Disabled.Height);
            }

            button.Selected = selected;
            button.Visible = visible;
            Buttons.Add(button);

            OnAddButton?.Invoke(this, EventArgs.Empty);
        }

        public void AddPicture(long id, string name, Texture2D texture, Region region, Color color, bool visible)
        {
            Picture picture = new Picture();
            picture.ID = id;
            picture.Name = name;
            picture.Texture = texture;
            picture.Region = region;
            picture.Image = new Rectangle(0, 0, picture.Texture.Width, picture.Texture.Height);
            picture.DrawColor = color;
            picture.Visible = visible;
            Pictures.Add(picture);

            OnAddPicture?.Invoke(this, EventArgs.Empty);
        }

        public void AddLabel(SpriteFont font, long id, string name, string text, Color text_color, Region region, bool visible)
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

            OnAddLabel?.Invoke(this, EventArgs.Empty);
        }

        public void AddLabel(SpriteFont font, long id, string name, string text, Color text_color, Texture2D texture, Region region, bool visible)
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

            OnAddLabel?.Invoke(this, EventArgs.Empty);
        }

        public void AddLabel(SpriteFont font, long id, string name, string text, Color text_color, Texture2D texture, Region region, Color draw_color, bool visible)
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

            OnAddLabel?.Invoke(this, EventArgs.Empty);
        }

        public void AddProgressBar(long id, string name, int max, int value, int increment, Texture2D base_texture, Texture2D bar_texture, Region region, Color color, bool visible)
        {
            ProgressBar progressBar = new ProgressBar();
            progressBar.ID = id;
            progressBar.Name = name;
            progressBar.Value = value;
            progressBar.Rate = increment;
            progressBar.Max_Value = max;
            progressBar.Base_Region = region;
            progressBar.Bar_Region = new Region(region.X, region.Y, 0, region.Height);
            progressBar.Base_Texture = base_texture;
            progressBar.Bar_Texture = bar_texture;
            progressBar.Bar_Image = new Rectangle(0, 0, 0, progressBar.Base_Texture.Height);
            progressBar.DrawColor = color;
            progressBar.Visible = visible;
            progressBar.Update();

            ProgressBars.Add(progressBar);

            OnAddProgressBar?.Invoke(this, EventArgs.Empty);
        }

        public void AddInput(SpriteFont font, long id, int max_length, string name, string text, Color text_color, Texture2D texture, Region region, bool active, bool visible)
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
            input.Region = region;
            input.Image = new Rectangle(0, 0, input.Texture.Width, input.Texture.Height);
            input.Visible = visible;
            input.Opacity = 0.8f;
            input.Active = active;
            Inputs.Add(input);

            OnAddInput?.Invoke(this, EventArgs.Empty);
        }

        public Button GetButton(string name)
        {
            int count = Buttons.Count;
            for (int i = 0; i < count; i++)
            {
                Button existing = Buttons[i];
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public Button GetButton(long id)
        {
            int count = Buttons.Count;
            for (int i = 0; i < count; i++)
            {
                Button existing = Buttons[i];
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public Button GetButton_Current()
        {
            int count = Buttons.Count;
            for (int i = 0; i < count; i++)
            {
                Button existing = Buttons[i];
                if (existing.ID == Current_Button)
                {
                    return existing;
                }
            }

            return null;
        }

        public Label GetLabel(string name)
        {
            int count = Labels.Count;
            for (int i = 0; i < count; i++)
            {
                Label existing = Labels[i];
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public Label GetLabel(long id)
        {
            int count = Labels.Count;
            for (int i = 0; i < count; i++)
            {
                Label existing = Labels[i];
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public Picture GetPicture(string name)
        {
            int count = Pictures.Count;
            for (int i = 0; i < count; i++)
            {
                Picture existing = Pictures[i];
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public Picture GetPicture(long id)
        {
            int count = Pictures.Count;
            for (int i = 0; i < count; i++)
            {
                Picture existing = Pictures[i];
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public Slider GetSlider(string name)
        {
            int count = Sliders.Count;
            for (int i = 0; i < count; i++)
            {
                Slider existing = Sliders[i];
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public Slider GetSlider(long id)
        {
            int count = Sliders.Count;
            for (int i = 0; i < count; i++)
            {
                Slider existing = Sliders[i];
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public InputBox GetInput(string name)
        {
            int count = Inputs.Count;
            for (int i = 0; i < count; i++)
            {
                InputBox existing = Inputs[i];
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public InputBox GetInput(long id)
        {
            int count = Inputs.Count;
            for (int i = 0; i < count; i++)
            {
                InputBox existing = Inputs[i];
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public ProgressBar GetProgressBar(string name)
        {
            int count = ProgressBars.Count;
            for (int i = 0; i < count; i++)
            {
                ProgressBar existing = ProgressBars[i];
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public ProgressBar GetProgressBar(long id)
        {
            int count = ProgressBars.Count;
            for (int i = 0; i < count; i++)
            {
                ProgressBar existing = ProgressBars[i];
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
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
