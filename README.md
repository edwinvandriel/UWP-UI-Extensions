### UWP UI Extensions

This is a library with UWP UI extensions for Windows 10 Anniversary Edition (Build 14393 and higher) with Visual Studio 2017.

Start using the library with including the namespace on your page like <pre><code>xmlns:extend="using:EvD.Uwp.UI.Extensions"</code></pre>

<br/>

#### ListViewBase extensions

Extend with an alternate row color brush
<pre>
	<code>
&lt;ListView 
	extend:ListViewBaseExtensions.AlternateRowColor="LightGray"&gt;
&lt;/ListView&gt;
	</code>
</pre>

Extend with a custom alternate itemtemplate
<pre>
	<code>
&lt;ListView 
	extend:ListViewBaseExtensions.AlternateRowItemTemplate="{StaticResource AlternateTemplate}"&gt;
&lt;/ListView&gt;
	</code>
</pre>

Extend with a controltemplate when your bound items are count zero
<pre>
	<code>
&lt;ListView 
	extend:ListViewBaseExtensions.EmptyDataControlTemplate="{StaticResource EmptyTemplate}"&gt;
&lt;/ListView&gt;
	</code>
</pre>

<br/>

#### UIElement extensions

Extend a UIElement with the accesskey hint. When you've set the AccessKey on a control you get a hint when pressing the ALT key.
For more information on the AccessHint property you can go to [Microsoft docs](https://docs.microsoft.com/en-us/windows/uwp/input-and-devices/access-keys).
Most of the sample code for the hint is used in this extension.

<pre>
	<code>
&lt;Button
	AccessKey="AR" 
	extend:UIElementExtensions.ShowAccessKeyHint="True"&gt;
&lt;/Button&gt;
	</code>
</pre>



