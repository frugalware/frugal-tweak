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

using Gtk;
using WebKit;
 
public class ValaBrowser : Window {
 
    private const string TITLE = "Frugalware Tweak Browser";
    private const string HOME_URL = "http://www.frugalware.org/";
    private const string DEFAULT_PROTOCOL = "http";
 
    private Regex protocol_regex;
 
    private Entry url_bar;
    private WebView web_view;
    private Label status_bar;
    private ToolButton back_button;
    private ToolButton forward_button;
    private ToolButton reload_button;
 
    public ValaBrowser () {
        this.title = ValaBrowser.TITLE;
        set_default_size (800, 600);
 
        try {
            this.protocol_regex = new Regex (".*://.*");
        } catch (RegexError e) {
            critical ("%s", e.message);
        }
 
        create_widgets ();
        connect_signals ();
        this.url_bar.grab_focus ();
    }
 
    private void create_widgets () {
        var toolbar = new Toolbar ();
        this.back_button = new ToolButton.from_stock (STOCK_GO_BACK);
        this.forward_button = new ToolButton.from_stock (STOCK_GO_FORWARD);
        this.reload_button = new ToolButton.from_stock (STOCK_REFRESH);
        toolbar.add (this.back_button);
        toolbar.add (this.forward_button);
        toolbar.add (this.reload_button);
        this.url_bar = new Entry ();
        this.web_view = new WebView ();
        var scrolled_window = new ScrolledWindow (null, null);
        scrolled_window.set_policy (PolicyType.AUTOMATIC, PolicyType.AUTOMATIC);
        scrolled_window.add (this.web_view);
        this.status_bar = new Label ("Welcome");
        this.status_bar.xalign = 0;
        var vbox = new VBox (false, 0);
        vbox.pack_start (toolbar, false, true, 0);
        vbox.pack_start (this.url_bar, false, true, 0);
        vbox.add (scrolled_window);
        vbox.pack_start (this.status_bar, false, true, 0);
        add (vbox);
    }
 
    private void connect_signals () {
        this.destroy.connect (Gtk.main_quit);
        this.url_bar.activate.connect (on_activate);
        this.web_view.title_changed.connect ((source, frame, title) => {
            this.title = "%s - %s".printf (title, ValaBrowser.TITLE);
        });
        this.web_view.load_committed.connect ((source, frame) => {
            this.url_bar.text = frame.get_uri ();
            update_buttons ();
        });
        this.back_button.clicked.connect (this.web_view.go_back);
        this.forward_button.clicked.connect (this.web_view.go_forward);
        this.reload_button.clicked.connect (this.web_view.reload);
    }
 
    private void update_buttons () {
        this.back_button.sensitive = this.web_view.can_go_back ();
        this.forward_button.sensitive = this.web_view.can_go_forward ();
    }
 
    private void on_activate () {
        var url = this.url_bar.text;
        if (!this.protocol_regex.match (url)) {
            url = "%s://%s".printf (ValaBrowser.DEFAULT_PROTOCOL, url);
        }
        this.web_view.open (url);
    }
 
    public void start () {
        show_all ();
        //this.web_view.open (ValaBrowser.HOME_URL);
    }
 
    public static int main (string[] args) { 

	Gtk.init (ref args); 
	/*commandline parameter handling*/
	string url=null;
        if(args.length>0)
        {
            url=args[1];
	}

	if(url==null)
		url=ValaBrowser.HOME_URL;
	
        var browser = new ValaBrowser ();
	browser.url_bar.text=url;
        browser.start ();
	browser.web_view.open (url);
        Gtk.main ();
	

        return 0;
    }
}
