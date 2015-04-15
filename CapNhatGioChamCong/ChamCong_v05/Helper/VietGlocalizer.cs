using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;

namespace ChamCong_v05.Helper {
	public class VietGridLocalizer : GridLocalizer {
		public override string Language { get { return "Deutsch"; } }
		public override string GetLocalizedString(GridStringId id) {
			string ret = string.Empty;
			switch (id) {
				case GridStringId.GridGroupPanelText: return "Kéo thả đầu cột vào đây để gom nhóm theo cột đó";
				case GridStringId.MenuColumnClearSorting: return "Sortierung entfernen";
				case GridStringId.MenuGroupPanelHide: return "Gruppierungsfeld ausblenden";
				case GridStringId.MenuColumnRemoveColumn: return "Spalte entfernen";
				case GridStringId.MenuColumnFilterEditor: return "Filter &bearbeiten";
				case GridStringId.MenuColumnFindFilterShow: return "Suche einblenden";
				case GridStringId.MenuColumnAutoFilterRowShow: return "Zeige Auto Filterzeile";
				case GridStringId.MenuColumnSortAscending: return "Aufsteigend sortieren";
				case GridStringId.MenuColumnSortDescending: return "Absteigend sortieren";
				case GridStringId.MenuColumnGroup: return "Gruppieren fur dieses Feld";
				case GridStringId.MenuColumnUnGroup: return "Gruppierung aufheben";
				case GridStringId.MenuColumnColumnCustomization: return "Laufzeit benutzerdefinierte Spalte";
				case GridStringId.MenuColumnBestFit: return "Optimale Breite";
				case GridStringId.MenuColumnFilter: return "Kann gruppieren";
				case GridStringId.MenuColumnClearFilter: return "Filter aufheben";
				case GridStringId.MenuColumnBestFitAllColumns: return "Optimale Breite (alle Spalten)";
				// ...  
				default:
					ret = base.GetLocalizedString(id);
					break;
			}
			return ret;
		}
	}

	public class GermanEditorsLocalizer : Localizer {
		public override string Language { get { return "Deutsch"; } }
		public override string GetLocalizedString(StringId id) {
			switch (id) {
				// ... 
				case StringId.NavigatorTextStringFormat: return "Zeile {0} von {1}";
				case StringId.PictureEditMenuCut: return "Ausschneiden";
				case StringId.PictureEditMenuCopy: return "Kopieren";
				case StringId.PictureEditMenuPaste: return "Einfugen";
				case StringId.PictureEditMenuDelete: return "Loschen";
				case StringId.PictureEditMenuLoad: return "Laden";
				case StringId.PictureEditMenuSave: return "Speichern";
				// ... 
			}
			return "";
		}
	}
}
