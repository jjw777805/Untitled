using MyManager;

namespace MyUI
{
    public class AudioSlider:Slider
    {
        public string paramName;

        protected override void Initial()
        {
            base.Initial();
            if (AudioManager.instance != null)
            {
                SetValue(AudioManager.instance.config.dic[paramName]);
            }
        }
        public override void SetValue(float newValue)
        {
            base.SetValue(newValue);
            AudioManager.instance.SetVolumn(paramName,value);
        }
    }
}