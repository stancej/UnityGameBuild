using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Image image;
    [SerializeField] private GameObject menu;

    private int c_sprite = 0;
    private int m_sprites = 0;
    
    private void OnEnable()
    {
        c_sprite = 0;
        m_sprites = sprites.Count;
        CurrentImage();
    }

    public void CurrentImage()
    {
        try
        {
            image.sprite = sprites[c_sprite];
        }
        catch
        {
            GoToMenu();
        }
    }

    public void NextImage()
    {
        c_sprite += 1;
        if (c_sprite >= m_sprites)
            GoToMenu();
        else
            image.sprite = sprites[c_sprite];
    }
    
    public void PrevImage()
    {
        c_sprite -= 1;
        if (c_sprite < 0 || m_sprites == 0)
            GoToMenu();
        else
            image.sprite = sprites[c_sprite];
    }

    private void GoToMenu()
    {
        menu.SetActive(true);
        gameObject.SetActive(false);
    }
}
