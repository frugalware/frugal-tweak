// Copyright (C) 2011 by gaetan gourdin <bouleetbil@frogdev.info>

/* Licensed under the MIT license (copied from the http://en.wikipedia.org/wiki/MIT_License site)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

const StatusIconDispatcher = imports.ui.statusIconDispatcher;
const Panel = imports.ui.panel;

function main()
{
	StatusIconDispatcher.STANDARD_TRAY_ICON_IMPLEMENTATIONS['parcellite'] = 'parcellite';
	StatusIconDispatcher.STANDARD_TRAY_ICON_IMPLEMENTATIONS['frugalware-tweak2'] = 'frugalware-tweak2';
	StatusIconDispatcher.STANDARD_TRAY_ICON_IMPLEMENTATIONS['frugal-mono-tools'] = 'frugalware-tweak';
	StatusIconDispatcher.STANDARD_TRAY_ICON_IMPLEMENTATIONS['skype'] = 'skype';
	StatusIconDispatcher.STANDARD_TRAY_ICON_IMPLEMENTATIONS['gnote'] = 'gnote';
	StatusIconDispatcher.STANDARD_TRAY_ICON_IMPLEMENTATIONS['xchat'] = 'xchat';
	StatusIconDispatcher.STANDARD_TRAY_ICON_IMPLEMENTATIONS['pidgin'] = 'pidgin';
	// Disable bulit-in icon that you don't need.
	Panel.STANDARD_TRAY_ICON_SHELL_IMPLEMENTATION['a11y'] = '';   // Accessiability icon
}

