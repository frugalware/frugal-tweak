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

[CCode (cprefix = "", lower_case_cprefix = "", cheader_filename = "config.h")]
namespace Config {
        public const string GETTEXT_PACKAGE;
        public const string SPRITE_DIR;
        public const string BACKGROUND_DIR;
        public const string PACKAGE_DATADIR;
        public const string PACKAGE_LOCALEDIR;
        public const string PACKAGE_NAME;
        public const string PACKAGE_VERSION;
        public const string VERSION;
	public const string HOMEDIR;
	public const string CACHEDIR;
	public const string PLUGINSDIR;
}

