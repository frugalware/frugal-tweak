/*
 *
 * (C) 2010 bouleetbil <bouleetbil@frogdev.info>
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
 */

public static class GtkObj {

	//frugalware-tweak
	public static Gtk.CheckButton notif;
	public static Gtk.CheckButton update;
	public static Gtk.Entry host;

	public static Gtk.TreeView modules;
	public static Gtk.ListStore listmodel_modules;

	//mini pacman-g2
	public static  Gtk.Entry entry_search_pkg;
	public static  Gtk.ComboBox combobox_repo;
	public static Gtk.ListStore listmodel_repo;
	public static Gtk.TreeView tree_pkg;
	public static Gtk.ListStore listmodel_pkg;

}
