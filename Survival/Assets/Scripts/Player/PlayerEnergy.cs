using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerEnergy : MonoBehaviour
{
    private float _maxEnergy = 100;
    private float recoverySpeed = 2;
    private float energy;
    [SerializeField] private Image energyImage;
    [SerializeField] private Text energyText;
    [SerializeField] private AudioSource soundOfLackEnergy;
    private void Start()
    {

        energy = _maxEnergy;
        energyImage.fillAmount = energy / _maxEnergy;
        energyText.text = energy.ToString("0") + "/" + _maxEnergy.ToString("0");
    }
    private void Update()
    {
        if(energy<_maxEnergy)
            RecoveringEnergy();
    }
    void RecoveringEnergy()
    {
        energy += Time.deltaTime * recoverySpeed;
        energyImage.fillAmount = energy / _maxEnergy;
        energyText.text = energy.ToString("0") + "/" + _maxEnergy.ToString("0");
    }
    public void ChangeEnergy(float countOfEnergy)
    {
        if (energy < countOfEnergy)
        {
            //soundOfLackEnergy.Play();
            return;
        }
        energy -= countOfEnergy;
        energyImage.fillAmount = energy / _maxEnergy;
        energyText.text = energy.ToString("0") + "/" + _maxEnergy.ToString("0");
    }
}
