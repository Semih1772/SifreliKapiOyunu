using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SifreliKapi : MonoBehaviour
{
    [Header("UI Elemanları")]
    public TMP_InputField sifreGirisKutusu;
    public TMP_Text durumMetni;
    public TMP_Text hataliGirislerMetni;
    public GameObject sifrePaneli;

    [Header("Kapı Ayarları")]
    public Transform kapiTransform;
    public string dogruSifre = "1234";
    public Vector3 acikPozisyon = new Vector3(0, 5, 0);

    private bool kapiAcildi = false;
    private List<string> hataliSifreler = new List<string>();

    public void SifreyiKontrolEt()
    {
        if (kapiAcildi) return;

        string girilenSifre = sifreGirisKutusu.text;

        if (girilenSifre == dogruSifre)
        {
            kapiAcildi = true;
            durumMetni.text = "Şifre Doğru! Kapı Açılıyor...";
            durumMetni.color = Color.green;

            RaporuGoster();

            // Kapıyı yukarı doğru kaydırır
            kapiTransform.position += acikPozisyon;

            // 5 saniye sonra arayüzü kapatır
            Invoke("PaneliKapat", 5f);
        }
        else
        {
            hataliSifreler.Add(girilenSifre);
            durumMetni.text = "Hatalı Şifre! Tekrar Deneyin.";
            durumMetni.color = Color.red;
            sifreGirisKutusu.text = "";
        }
    }

    void RaporuGoster()
    {
        if (hataliGirislerMetni == null) return;

        if (hataliSifreler.Count == 0)
        {
            hataliGirislerMetni.text = "Tebrikler! Hiç hatalı deneme yapmadınız.";
        }
        else
        {
            string rapor = $"Toplam Hatalı Deneme: {hataliSifreler.Count}\nDenenen Şifreler:\n";
            for (int i = 0; i < hataliSifreler.Count; i++)
            {
                rapor += $"{i + 1}. Deneme: {hataliSifreler[i]}\n";
            }
            hataliGirislerMetni.text = rapor;
        }
    }

    void PaneliKapat()
    {
        if (sifrePaneli != null)
            sifrePaneli.SetActive(false);
    }
}