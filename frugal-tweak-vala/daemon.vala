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
using GLib;

class Deamon : GLib.Object {

    public static int main(string[] args) {

        stdout.printf("Start Frugalware Tweak Daemon\n");
	//timer
	Timer timer = new Timer();
	timer.start();
	
	stdout.printf("The program will close in three seconds\n");

	for(double x = 0.0; x <= 3.0;) {
	     x = timer.elapsed();
	}

        return 0;
    }
}
