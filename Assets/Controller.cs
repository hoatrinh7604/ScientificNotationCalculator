using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject inputField;
    [SerializeField] Button addButton;
    [SerializeField] Button continueButton;

    [SerializeField] TMP_InputField numbers;

    [SerializeField] GameObject[] listItemResults;

    [SerializeField] TextMeshProUGUI SN;
    [SerializeField] TextMeshProUGUI EN;
    [SerializeField] TextMeshProUGUI EngiN;
    [SerializeField] TextMeshProUGUI RN;

    [SerializeField] GameObject listFieldParent;

    [SerializeField] GameObject numberPrefab;
    [SerializeField] GameObject GCFObj;

    [SerializeField] Button calButton;

    private List<GameObject> list = new List<GameObject>();
    private List<double> listInt = new List<double>();
    private int amount = 0;

    public string CURRENCY_FORMAT = "#,##0.00";
    public NumberFormatInfo NFI = new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." };

    private int type = 1;

    [SerializeField] Color[] listColor;

    //Singleton
    public static Controller Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        Clear();
    }

    private void DropdownitemSelected()
    {
        
    }

    private void DropdownitemAgeSelected()
    {
        
    }


    public void OnValueChanged()
    {
        if(CheckValidate())
        {
            calButton.interactable = true;
        }
        else
        {
            calButton.interactable= false;
        }
    }

    List<int> listNumbers = new List<int>();
    private bool CheckValidate()
    {
        if (numbers.text == "")
        {
            return false;
        }

        
        return true;
    }


    public void Sum()
    {
        CalWithAdult();
        listFieldParent.SetActive(true);
    }

    private void CalWithAdult()
    {
        // Real Number
        RN.text = numbers.text;
        // Find Scientific Notation
        int leng = 0;
        string remove = ".";
        if(numbers.text.IndexOf('.') >= 0)
        {
            leng = numbers.text.IndexOf('.') - 1;
            numbers.text = numbers.text.Replace(remove, "");
        }
        else
        {
            leng = numbers.text.Length - 1;
        }

        SN.text = numbers.text.Insert(1, ".") + " x 10^" + leng;
        EN.text = numbers.text.Insert(1, ".") + "e" + leng;

        // Find EngiN
        int temp = leng % 3;
        int boi = temp * 3;
        EngiN.text = numbers.text.Insert(1 + leng-boi, ".") + " x 10^" + boi;

    }

    public bool CalcIsPrime(int number)
    {

        if (number == 1) return false;
        if (number == 2) return true;

        if (number % 2 == 0) return false; // Even number     

        for (int i = 2; i < number; i++)
        { // Advance from two to include correct calculation for '4'
            if (number % i == 0) return false;
        }

        return true;

    }

    public void Continue()
    {
        if(amount==0) return;
        Clear();
    }

    public void Clear()
    {
        listFieldParent.SetActive(false);

        numbers.text = "";

        calButton.interactable = false;
    }

    public void Quit()
    {
        Clear();
        Application.Quit();
    }
}
