#!/usr/bin/env python

#/*
# * Copyright (c) 2010 gaetan gourdin
# *
# * Permission is hereby granted, free of charge, to any person obtaining a copy
# * of this software and associated documentation files (the "Software"), to deal
# * in the Software without restriction, including without limitation the rights
# * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# * copies of the Software, and to permit persons to whom the Software is
# * furnished to do so, subject to the following conditions:
# *
# * The above copyright notice and this permission notice shall be included in
# * all copies or substantial portions of the Software.
# *
# * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
# * THE SOFTWARE.
# */

import sys,os

ConfigKeymap = "/etc/sysconfig/keymap"
ConfigLang = "/etc/profile.d/lang.sh"
#ConfigLang = "lang.sh"
#ConfigKeymap = "keymap"

def replace(section,value,configfile):
	if os.path.exists(configfile)==1:
		import codecs
		file = codecs.open(configfile,"r","utf-8")
		text=""
		for line in file:
			if line.find(section)==0:
				line=value+"\n"
			text=text+line
		FILE = open(configfile,"w")
		FILE.writelines(text)


def print_menu():
	print ("language...")
	print ("1. pt_BR - Brazilian Portuguese")
	print ("2. cz_CZ - Czech")
	print ("3. da_NK - Danish")
	print ("4. nl_NL - Dutch")
	print ("5. en_US - English")
	print ("6. fr_FR - French")
	print ("7. de_DE - German")
	print ("8. hu_HU - Hungarian")
	print ("9. id_ID - Indonesian")
	print ("10. it_IT - Italian")
	print ("11. ro_RO - Romanian")
	print ("12. sk_SK - Slovak")
	print ("13. sv_SE - Swedish")
	x = input("Select your language: ")
	if x == 1:
		replace("export LANG","export LANG=pt_BR",ConfigLang)
		replace("keymap=","keymap=",ConfigKeymap)
		os.system("updatexorg.sh")
	elif x == 2:
		replace("export LANG","export LANG=cz_CZ",ConfigLang)
		replace("keymap=","keymap=",ConfigKeymap)
		os.system("updatexorg.sh")
	elif x == 3:
		replace("export LANG","export LANG=da_NK",ConfigLang)
		replace("keymap=","keymap=",ConfigKeymap)
		os.system("updatexorg.sh")
	elif x == 4:
		replace("export LANG","export LANG=nl_NL",ConfigLang)
		replace("keymap=","keymap=",ConfigKeymap)
		os.system("updatexorg.sh")
	elif x == 5:
		replace("export LANG","export LANG=en_US",ConfigLang)
		replace("keymap=","keymap=",ConfigKeymap)
		os.system("updatexorg.sh")
	elif x == 6:
		replace("export LANG","export LANG=fr_FR",ConfigLang)
		replace("keymap=","keymap=fr-latin9",ConfigKeymap)
		os.system("updatexorg.sh")
	elif x == 7:
		replace("export LANG","export LANG=de_DE",ConfigLang)
		replace("keymap=","keymap=",ConfigKeymap)
		os.system("updatexorg.sh")
	elif x == 8:
		replace("export LANG","export LANG=hu_HU",ConfigLang)
		replace("keymap=","keymap=",ConfigKeymap)
		os.system("updatexorg.sh")
	elif x == 9:
		replace("export LANG","export LANG=id_ID",ConfigLang)
		replace("keymap=","keymap=",ConfigKeymap)
		os.system("updatexorg.sh")
	elif x == 10:
		replace("export LANG","export LANG=it_IT",ConfigLang)
		replace("keymap=","keymap=",ConfigKeymap)
		os.system("updatexorg.sh")
	elif x == 11:
		replace("export LANG","export LANG=ro_RO",ConfigLang)
		replace("keymap=","keymap=",ConfigKeymap)
		os.system("updatexorg.sh")
	elif x == 12:
		replace("export LANG","export LANG=sk_SK",ConfigLang)
		replace("keymap=","keymap=",ConfigKeymap)
		os.system("updatexorg.sh")
	elif x == 13:
		replace("export LANG","export LANG=sv_SE",ConfigLang)
		replace("keymap=","keymap=",ConfigKeymap)
		os.system("updatexorg.sh")
	else:
		print_menu()
print_menu()

sys.exit()
