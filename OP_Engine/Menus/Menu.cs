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

        public List<Button> Buttons;
        public List<Picture> Pictures;
        public List<Label> Labels;
        public List<Slider> Sliders;
        public List<InputBox> Inputs;
        public List<ProgressBar> ProgressBars;

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

        public Menu() : base()
        {
            Buttons = new List<Button>();
            Pictures = new List<Picture>();
            Labels = new List<Label>();
            Sliders = new List<Slider>();
            Inputs = new List<InputBox>();
            ProgressBars = new List<ProgressBar>();
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

        public void AddButton(ButtonOptions options)
        {
            Button button = new Button
            {
                ID = options.id,
                Name = options.name,
                Text = options.text,
                HoverText = options.hover_text,
                Font = options.font,
                TextColor = options.text_color,
                TextColor_Selected = options.text_selected_color,
                TextColor_Disabled = options.text_disabled_color,
                DrawColor = options.draw_color,
                DrawColor_Selected = options.draw_color_selected,
                DrawColor_Disabled = options.draw_color_disabled,
                Texture = options.texture,
                Texture_Highlight = options.texture_highlight,
                Texture_Disabled = options.texture_disabled,
                Region = options.region,
                Enabled = options.enabled,
                Selected = options.selected,
                Visible = options.visible
            };

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

            Buttons.Add(button);

            OnAddButton?.Invoke(this, EventArgs.Empty);
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

        public void AddPicture(PictureOptions options)
        {
            Picture picture = new Picture
            {
                ID = options.id,
                Name = options.name,
                Texture = options.texture,
                Region = options.region,
                DrawColor = options.color,
                Visible = options.visible
            };

            if (picture.Texture != null)
            {
                picture.Image = new Rectangle(0, 0, picture.Texture.Width, picture.Texture.Height);
            }

            Pictures.Add(picture);

            OnAddPicture?.Invoke(this, EventArgs.Empty);
        }

        public void AddPicture(long id, string name, Texture2D texture, Region region, Color color, bool visible)
        {
            Picture picture = new Picture
            {
                ID = id,
                Name = name,
                Texture = texture,
                Region = region,
                DrawColor = color,
                Visible = visible
            };

            if (picture.Texture != null)
            {
                picture.Image = new Rectangle(0, 0, picture.Texture.Width, picture.Texture.Height);
            }

            Pictures.Add(picture);

            OnAddPicture?.Invoke(this, EventArgs.Empty);
        }

        public void AddLabel(LabelOptions options)
        {
            Label label = new Label
            {
                ID = options.id,
                Font = options.font,
                Name = options.name,
                Text = options.text,
                TextColor = options.text_color,
                Alignment_Verticle = options.alignment_verticle,
                Alignment_Horizontal = options.alignment_horizontal,
                Texture = options.texture,
                Region = options.region,
                DrawColor = options.draw_color,
                Visible = options.visible
            };

            if (label.Texture != null)
            {
                label.Image = new Rectangle(0, 0, label.Texture.Width, label.Texture.Height);
            }

            Labels.Add(label);

            OnAddLabel?.Invoke(this, EventArgs.Empty);
        }

        public void AddLabel(SpriteFont font, long id, string name, string text, Color text_color, Region region, bool visible)
        {
            Label label = new Label
            {
                ID = id,
                Name = name,
                Value = 255,
                Text = text,
                TextColor = text_color,
                Font = font,
                Region = region,
                Visible = visible
            };

            Labels.Add(label);

            OnAddLabel?.Invoke(this, EventArgs.Empty);
        }

        public void AddLabel(SpriteFont font, long id, string name, string text, Color text_color, Texture2D texture, Region region, bool visible)
        {
            Label label = new Label
            {
                ID = id,
                Name = name,
                Text = text,
                Font = font,
                TextColor = text_color,
                DrawColor = Color.White,
                Texture = texture,
                Region = region,
                Visible = visible
            };

            if (label.Texture != null)
            {
                label.Image = new Rectangle(0, 0, label.Texture.Width, label.Texture.Height);
            }

            Labels.Add(label);

            OnAddLabel?.Invoke(this, EventArgs.Empty);
        }

        public void AddLabel(SpriteFont font, long id, string name, string text, Color text_color, Texture2D texture, Region region, Color draw_color, bool visible)
        {
            Label label = new Label
            {
                ID = id,
                Name = name,
                Text = text,
                Font = font,
                TextColor = text_color,
                DrawColor = draw_color,
                Texture = texture,
                Region = region,
                Visible = visible
            };

            if (label.Texture != null)
            {
                label.Image = new Rectangle(0, 0, label.Texture.Width, label.Texture.Height);
            }

            Labels.Add(label);

            OnAddLabel?.Invoke(this, EventArgs.Empty);
        }

        public void AddProgressBar(ProgressBarOptions options)
        {
            ProgressBar progressBar = new ProgressBar
            {
                ID = options.id,
                Name = options.name,
                Max_Value = options.max,
                Value = options.value,
                Rate = options.increment,
                Base_Texture = options.base_texture,
                Bar_Texture = options.bar_texture,
                Base_Region = options.region,
                DrawColor = options.draw_color,
                Visible = options.visible
            };

            if (progressBar.Base_Region != null)
            {
                progressBar.Bar_Region = new Region(progressBar.Base_Region.X, progressBar.Base_Region.Y, 0, progressBar.Base_Region.Height);
            }

            if (progressBar.Base_Texture != null)
            {
                progressBar.Bar_Image = new Rectangle(0, 0, 0, progressBar.Base_Texture.Height);
            }

            progressBar.Update();

            ProgressBars.Add(progressBar);

            OnAddProgressBar?.Invoke(this, EventArgs.Empty);
        }

        public void AddProgressBar(long id, string name, int max, int value, int increment, Texture2D base_texture, Texture2D bar_texture, Region region, Color draw_color, bool visible)
        {
            ProgressBar progressBar = new ProgressBar
            {
                ID = id,
                Name = name,
                Max_Value = max,
                Value = value,
                Rate = increment,
                Base_Texture = base_texture,
                Bar_Texture = bar_texture,
                Base_Region = region,
                DrawColor = draw_color,
                Visible = visible
            };

            if (progressBar.Base_Region != null)
            {
                progressBar.Bar_Region = new Region(progressBar.Base_Region.X, progressBar.Base_Region.Y, 0, progressBar.Base_Region.Height);
            }

            if (progressBar.Base_Texture != null)
            {
                progressBar.Bar_Image = new Rectangle(0, 0, 0, progressBar.Base_Texture.Height);
            }

            progressBar.Update();

            ProgressBars.Add(progressBar);

            OnAddProgressBar?.Invoke(this, EventArgs.Empty);
        }

        public void AddInput(InputBoxOptions options)
        {
            InputBox input = new InputBox
            {
                ID = options.id,
                MaxLength = options.max_length,
                Name = options.name,
                Text = options.text,
                Font = options.font,
                TextColor = options.text_color,
                DrawColor = options.draw_color,
                Texture = options.texture,
                Region = options.region,
                Visible = options.visible,
                Opacity = options.opacity,
                Active = options.active
            };

            if (input.Texture != null)
            {
                input.Image = new Rectangle(0, 0, input.Texture.Width, input.Texture.Height);
            }

            Inputs.Add(input);

            OnAddInput?.Invoke(this, EventArgs.Empty);
        }

        public void AddInput(SpriteFont font, long id, int max_length, string name, string text, Color text_color, Texture2D texture, Region region, bool active, bool visible)
        {
            InputBox input = new InputBox
            {
                ID = id,
                MaxLength = max_length,
                Name = name,
                Text = text,
                Font = font,
                TextColor = text_color,
                DrawColor = Color.White,
                Texture = texture,
                Region = region,
                Visible = visible,
                Opacity = 0.8f,
                Active = active
            };

            if (input.Texture != null)
            {
                input.Image = new Rectangle(0, 0, input.Texture.Width, input.Texture.Height);
            }

            Inputs.Add(input);

            OnAddInput?.Invoke(this, EventArgs.Empty);
        }

        public void AddInput(SpriteFont font, long id, int max_length, string name, string text, Color text_color, Color draw_color, Texture2D texture, Region region, bool active, bool visible)
        {
            InputBox input = new InputBox
            {
                ID = id,
                MaxLength = max_length,
                Name = name,
                Text = text,
                Font = font,
                TextColor = text_color,
                DrawColor = draw_color,
                Texture = texture,
                Region = region,
                Visible = visible,
                Opacity = 0.8f,
                Active = active
            };

            if (input.Texture != null)
            {
                input.Image = new Rectangle(0, 0, input.Texture.Width, input.Texture.Height);
            }

            Inputs.Add(input);

            OnAddInput?.Invoke(this, EventArgs.Empty);
        }

        public void AddInput(SpriteFont font, long id, int max_length, string name, string text, Color text_color, Texture2D texture, Region region, float opacity, bool active, bool visible)
        {
            InputBox input = new InputBox
            {
                ID = id,
                MaxLength = max_length,
                Name = name,
                Text = text,
                Font = font,
                TextColor = text_color,
                DrawColor = Color.White,
                Texture = texture,
                Region = region,
                Visible = visible,
                Opacity = opacity,
                Active = active
            };

            if (input.Texture != null)
            {
                input.Image = new Rectangle(0, 0, input.Texture.Width, input.Texture.Height);
            }

            Inputs.Add(input);

            OnAddInput?.Invoke(this, EventArgs.Empty);
        }

        public void AddInput(SpriteFont font, long id, int max_length, string name, string text, Color text_color, Color draw_color, Texture2D texture, Region region, float opacity, bool active, bool visible)
        {
            InputBox input = new InputBox
            {
                ID = id,
                MaxLength = max_length,
                Name = name,
                Text = text,
                Font = font,
                TextColor = text_color,
                DrawColor = draw_color,
                Texture = texture,
                Region = region,
                Visible = visible,
                Opacity = opacity,
                Active = active
            };

            if (input.Texture != null)
            {
                input.Image = new Rectangle(0, 0, input.Texture.Width, input.Texture.Height);
            }

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
