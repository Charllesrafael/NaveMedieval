using TotemServices.DNA;
using UnityEngine;

[System.Serializable]
public class TotemDNAItemNephe
{
    public string _name;
    public Color primary_color;
    public string classical_element;
    public int chassi_nd;
    public int cockpit_nd;
    public int thruster_nd;
    public int wings_nd;

    public override string ToString()
    {
        return $"classical_element: {classical_element} | chassi_nd: {chassi_nd} | cockpit_nd: {cockpit_nd} | cockpit_nd: {thruster_nd} | cockpit_nd: {wings_nd}| " +
                $"primary_color: {primary_color}";

    }

    public static TotemDNAFilter NepheItemFilter
    {
        get
        {
            return new TotemDNAFilter(Resources.Load<TextAsset>(SmartContractItemsFilterNameNephe).text);
        }
    }

    public const string SmartContractItemsFilterNameNephe = "totem-common-files/filters/totem-item-nephe";
}
