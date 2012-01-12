#!/usr/bin/python
#
# Copyright (C) gaetan gourdin 2011 <bouleetbil@frogdev.info>
# 
# pyfpm is free software: you can redistribute it and/or modify it
# under the terms of the GNU General Public License as published by the
# Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
# 
# pyfpm is distributed in the hope that it will be useful, but
# WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
# See the GNU General Public License for more details.
# 
# You should have received a copy of the GNU General Public License along
# with this program.  If not, see <http://www.gnu.org/licenses/>.


from gi.repository import Gtk, GdkPixbuf, Gdk
import os, sys
from pyfpmtools.tools import *

pyconfig=configuration()
suxcommande=pyconfig.Read('configuration','sux')
if suxcommande=="":
	suxcommande="gksu"

class GUI:
	def __init__(self):
		
		self.builder = Gtk.Builder()
		self.builder.add_from_file(UI_PYFPMCONF)
		self.builder.connect_signals(self)
		self.window = self.builder.get_object('window')
		if(self.window):
			self.window.connect("destroy",Gtk.main_quit)
		self.SAI_SUX=self.builder.get_object("SAI_SUX") 
		self.SAI_SUX.set_text(suxcommande)
		self.window.show_all()

	def destroy(window, self):
		Gtk.main_quit()

	def on_BTN_OK_clicked(self,widget):
		suxcommande=self.SAI_SUX.get_text()
		pyconfig.Write('configuration','sux',suxcommande)
		sys.exit()
		
def main():
	app = GUI()
	Gtk.main()
		
if __name__ == "__main__":
    sys.exit(main())
