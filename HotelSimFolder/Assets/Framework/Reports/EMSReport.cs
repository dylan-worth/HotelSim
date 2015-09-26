using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EMSReport : MonoBehaviour {

	public InputField happinessModifierField;
	public InputField monthlyCostsField;

	float happinessModifier;
	float monthlyCosts;
	
	[SerializeField]float emsBaseCostRecycling;
	[Range(-5f,5f)]
	[SerializeField]float emsBaseHappyModRecyling;
	float emsCostRecycling;
	float emsMODRecycling;
	[SerializeField]float emsBaseCostGarden;
	[Range(-5f,5f)]
	[SerializeField]float emsBaseHappyModGarden;
	float emsCostGarden;
	float emsMODGarden;
	[SerializeField]float emsBaseCostWC;
	[Range(-5f,5f)]
	[SerializeField]float emsBaseHappyModWC;
	float emsCostWC;
	float emsMODWC;
	[SerializeField]float emsBaseCostReUse;
	[Range(-5f,5f)]
	[SerializeField]float emsBaseHappyModReUse;
	float emsCostReUse;
	float emsMODReUse;
	[SerializeField]float emsBaseCostShowers;
	[Range(-5f,5f)]
	[SerializeField]float emsBaseHappyModShowers;
	float emsCostShowers;
	float emsMODShowers;
	[SerializeField]float emsBaseCostInsulation;
	[Range(-5f,5f)]
	[SerializeField]float emsBaseHappyModInsulation;
	float emsCostInsulation;
	float emsMODInsulation;
	[SerializeField]float emsBaseCostSelfGeneration;
	[Range(-5f,5f)]
	[SerializeField]float emsBaseHappyModSelfGeneration;
	float emsCostSelfGeneration;
	float emsMODSelfGeneration;
	[SerializeField]float emsBaseCostLighting;
	[Range(-5f,5f)]
	[SerializeField]float emsBaseHappyModLighting;
	float emsCostLighting;
	float emsMODLighting;
	[SerializeField]float emsBaseCostSolarPower;
	[Range(-5f,5f)]
	[SerializeField]float emsBaseHappyModSolarPower;
	float emsCostSolarPower;
	float emsMODSolarPower;
	[SerializeField]float emsBaseCostElectricCars;
	[Range(-5f,5f)]
	[SerializeField]float emsBaseHappyModElectricCars;
	float emsCostElectricCars;
	float emsMODElectricCars;

	public float GetMonthlyEMSCosts()
	{
		return monthlyCosts;
	}
	void RefreshDisplay()
	{
		CalculateTotals();
		happinessModifierField.text = happinessModifier.ToString();
		monthlyCostsField.text = monthlyCosts.ToString();
		MasterReference.emsModifier = happinessModifier;
	}
	void CalculateTotals()
	{
		monthlyCosts = emsCostRecycling+emsCostGarden+emsCostWC+emsCostReUse+
			emsCostShowers+emsCostInsulation+emsCostSelfGeneration+emsCostLighting+
				emsCostSolarPower+emsCostElectricCars;
		happinessModifier = emsMODRecycling+emsMODGarden+emsMODWC+emsMODReUse+emsMODShowers+
			emsMODInsulation+emsMODSelfGeneration+emsMODLighting+emsMODSolarPower+emsMODElectricCars;
	}
	public void Recycling(int tier)
	{
		emsCostRecycling = emsBaseCostRecycling*tier;
		emsMODRecycling = emsMODRecycling*tier;
		RefreshDisplay();
	}
	public void Garden(int tier)
	{
		emsCostGarden = emsBaseCostGarden*tier;
		emsMODGarden = emsBaseHappyModGarden*tier;
		RefreshDisplay();
	}
	public void WC(int tier)
	{
		emsCostWC = emsBaseCostWC*tier;
		emsMODWC = emsBaseHappyModWC*tier;
		RefreshDisplay();
	}
	public void ReUse(int tier)
	{
		emsCostReUse = emsBaseCostReUse*tier;
		emsMODReUse = emsBaseHappyModReUse*tier;
		RefreshDisplay();
	}
	public void Showers(int tier)
	{
		emsCostShowers = emsBaseCostShowers*tier;
		emsMODShowers = emsBaseHappyModShowers*tier;
		RefreshDisplay();
	}
	public void Insultation(int tier)
	{
		emsCostInsulation = emsBaseCostInsulation*tier;
		emsMODInsulation = emsBaseHappyModInsulation*tier;
		RefreshDisplay();
	}
	public void SelfGeneration(int tier)
	{
		emsCostSelfGeneration = emsBaseCostSelfGeneration*tier;
		emsMODSelfGeneration = emsBaseHappyModSelfGeneration*tier;
		RefreshDisplay();
	}
	public void Lighting(int tier)
	{
		emsCostLighting = emsBaseCostLighting*tier;
		emsMODLighting = emsBaseHappyModLighting*tier;
		RefreshDisplay();
	}
	public void SolarPower(int tier)
	{
		emsCostSolarPower = emsBaseCostSolarPower*tier;
		emsMODSolarPower = emsBaseHappyModSolarPower*tier;
		RefreshDisplay();
	}
	public void ElectricCars(int tier)
	{
		emsCostElectricCars = emsBaseCostElectricCars*tier;
		emsMODElectricCars = emsBaseHappyModElectricCars*tier;
		RefreshDisplay();
	}
}