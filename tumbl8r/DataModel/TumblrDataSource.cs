using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Net.Http;
using Windows.Data.Json;
using Windows.ApplicationModel;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Storage;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace tumbl8r.Data
{
    /// <summary>
    /// Base class for <see cref="TumblrTextDataItem"/>,  <see cref="TumblrPhotoDataItem"/>, <see cref="TumblrQuoteDataItem"/>, 
    /// <see cref="TumblrLinkDataItem"/>, <see cref="TumblrChatDataItem"/>, <see cref="TumblrAudioDataItem"/>, 
    /// <see cref="TumblrVideoDataItem"/>, <see cref="TumblrAnswerDataItem"/>, and <see cref="TumblrDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class TumblrDataCommon : tumbl8r.Common.BindableBase
    {

        internal static Uri _baseUri = new Uri("ms-appx:///");
        
        public static string STR_BLOG_NAME = "blog_name";
        public static string STR_ID = "id";
        public static string STR_POST_URL = "post_url";
        public static string STR_TYPE = "type";
        public static string STR_TIMESTAMP = "timestamp";
        public static string STR_DATE = "date";
        public static string STR_FORMAT = "format";
        public static string STR_REBLOG_KEY = "reblog_key";
        public static string STR_TAGS = "tags";
        public static string STR_BOOKMARKLET = "bookmarklet";
        public static string STR_MOBILE = "mobile";
        public static string STR_SOURCE_URL = "source_url";
        public static string STR_SOURCE_TITLE = "source_title";
        public static string STR_LIKED = "liked";
        public static string STR_STATE = "state";
        public static string STR_TOTAL_POSTS = "total_posts";

        public TumblrDataCommon()
        {
            this._blog_name = string.Empty;
            this._bookmarklet = false;
            this._date = string.Empty;
            this._format = string.Empty;
            this._id = 0;
            this._liked = false;
            this._mobile = false;
            this._post_url = string.Empty;
            this._reblog_key = string.Empty;
            this._source_title = string.Empty;
            this._source_url = string.Empty;
            this._state = string.Empty;
            this._tags = new Collection<string>();
            this._timestamp = 0;
            this._total_posts = 0;
            this._type = string.Empty;
        }

        public TumblrDataCommon(String jsonstring)
        {
            JsonObject root = JsonObject.Parse(jsonstring);
            this._blog_name = root.GetNamedString(STR_BLOG_NAME);
            this._bookmarklet = root.GetNamedBoolean(STR_BOOKMARKLET);
            this._date = root.GetNamedString(STR_DATE);
            this._format = root.GetNamedString(STR_FORMAT);
            this._id = (long) root.GetNamedNumber(STR_ID);
            this._liked = root.GetNamedBoolean(STR_LIKED);
            this._mobile = root.GetNamedBoolean(STR_MOBILE);
            this._post_url = root.GetNamedString(STR_POST_URL);
            this._reblog_key = root.GetNamedString(STR_REBLOG_KEY);
            this._source_title = root.GetNamedString(STR_SOURCE_TITLE);
            this._source_url = root.GetNamedString(STR_SOURCE_URL);
            this._state = root.GetNamedString(STR_STATE);
            this._tags = new Collection<string>();
            setTagsFromArray(root.GetNamedArray(STR_TAGS));
            this._timestamp = (long) root.GetNamedNumber(STR_TIMESTAMP);
            this._total_posts = (long) root.GetNamedNumber(STR_TOTAL_POSTS);
            this._type = root.GetNamedString(STR_TYPE);
        }

        public TumblrDataCommon(String blogName, long id, String postURL, String type, long timestamp, String date, String format, String reblogKey,
                                String tags, bool bookmarkLet, bool mobile, String sourceURL, String sourceTitle, bool liked, 
                                String state, long totalPosts)
        {
            this._blog_name = blogName;
            this._bookmarklet = bookmarkLet;
            this._date = date;
            this._format = format;
            this._id = id;
            this._liked = liked;
            this._mobile = mobile;
            this._post_url = postURL;
            this._reblog_key = reblogKey;
            this._source_title = sourceTitle;
            this._source_url = sourceURL;
            this._state = state;
            this._tags = new Collection<string>();
            setTagsFromString(tags);
            this._timestamp = timestamp;
            this._total_posts = totalPosts;
            this._type = type;
        }

        private long _id = 0;
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _blog_name = string.Empty;
        public string Blog_name
        {
            get { return _blog_name; }
            set { _blog_name = value; }
        }

        private string _post_url = string.Empty;
        public string Post_url
        {
            get { return _post_url; }
            set { _post_url = value; }
        }


        private string _type = string.Empty;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private long _timestamp = 0;
        public long Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        private string _date = string.Empty;
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private string _format = string.Empty;
        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }

        private string _reblog_key = string.Empty;
        public string Reblog_key
        {
            get { return _reblog_key; }
            set { _reblog_key = value; }
        }

        private Collection<string> _tags = null;
        public Collection<string> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        private bool _bookmarklet = false;
        public bool Bookmarklet
        {
            get { return _bookmarklet; }
            set { _bookmarklet = value; }
        }

        private bool _mobile = false;
        public bool Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        private string _source_url = string.Empty;
        public string Source_url
        {
            get { return _source_url; }
            set { _source_url = value; }
        }

        private string _source_title = string.Empty;
        public string Source_title
        {
            get { return _source_title; }
            set { _source_title = value; }
        }

        private bool _liked = false;
        public bool Liked
        {
            get { return _liked; }
            set { _liked = value; }
        }

        private string _state = string.Empty;
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        private long _total_posts = 0;
        public long Total_posts
        {
            get { return _total_posts; }
            set { _total_posts = value; }
        }

        private void setTagsFromString(String jsontags)
        {
            var items = JsonArray.Parse(jsontags);
            foreach (var item in items) 
            {
                this._tags.Add(item.ToString());
            }
        }

        private void setTagsFromArray(JsonArray arr)
        {
            foreach (JsonObject item in arr)
            {
                this._tags.Add(item.ToString());
            }
        }
    }

    /// <summary>
    /// Tumblr text item data model.
    /// </summary>
    public class TumblrTextDataItem : TumblrDataCommon
    {
        public static string STR_TITLE = "title";
        public static string STR_BODY = "body";

        public TumblrTextDataItem()
            : base()
        {
        }

        public TumblrTextDataItem(String jsonstring)
            : base(jsonstring)
        {
            JsonObject root = JsonObject.Parse(jsonstring);
            this._body = root.GetNamedString(STR_BODY);
            this._title = root.GetNamedString(STR_TITLE);
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _body = string.Empty;
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }
    }

    /// <summary>
    /// Tumblr photo item data model.
    /// </summary>
    public class TumblrPhotoDataItem : TumblrDataCommon
    {
        public static string STR_PHOTOS = "photos";
        public static string STR_CAPTION = "caption";
        public static string STR_WIDTH = "width";
        public static string STR_HEIGHT = "height";
        public static string STR_ALT_SIZES = "alt_sizes";
        public static string STR_URL = "url";

        public TumblrPhotoDataItem()
            : base()
        {
        }

        public TumblrPhotoDataItem(String jsonstring)
            : base(jsonstring)
        {
            JsonObject root = JsonObject.Parse(jsonstring);
            this._caption = root.GetNamedString(STR_CAPTION);
            this._width = (int) root.GetNamedNumber(STR_WIDTH);
            this._height = (int) root.GetNamedNumber(STR_HEIGHT);
            setPhotosFromJsonArr(root.GetNamedArray(STR_PHOTOS));
        }

        private string _caption = string.Empty;
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        private int _width = 0;
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private int _height = 0;
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private Collection<PhotoItem> _photos = null;
        private Collection<PhotoItem> Photos
        {
            get { return _photos; }
            set { _photos = value; }
        }

        private void setPhotosFromJsonArr(JsonArray photos)
        {
            this._photos = new Collection<PhotoItem>();
            foreach (JsonObject photo in photos)
            {
                PhotoItem p = new PhotoItem();
                p.Caption = photo.GetNamedString(STR_CAPTION);
                p.Url = photo.GetNamedString(STR_URL);
                p.Width = (int) photo.GetNamedNumber(STR_WIDTH);
                p.Height = (int) photo.GetNamedNumber(STR_HEIGHT);
                this._photos.Add(p);
            }
        }

        private class PhotoItem
        {
            private string _caption;

            public string Caption
            {
                get { return _caption; }
                set { _caption = value; }
            }
            private int _width;

            public int Width
            {
                get { return _width; }
                set { _width = value; }
            }
            private int _height;

            public int Height
            {
                get { return _height; }
                set { _height = value; }
            }
            private string _url;

            public string Url
            {
                get { return _url; }
                set { _url = value; }
            }
        }
    }

    /// <summary>
    /// Tumblr quote item data model.
    /// </summary>
    public class TumblrQuoteDataItem : TumblrDataCommon
    {
        public static string STR_TEXT = "text";
        public static string STR_SOURCE = "source";

        public TumblrQuoteDataItem()
            : base()
        {
        }

        public TumblrQuoteDataItem(String jsonstring)
            : base(jsonstring)
        {
            JsonObject root = JsonObject.Parse(jsonstring);
            this._source = root.GetNamedString(STR_SOURCE);
            this._text = root.GetNamedString(STR_TEXT);
        }

        private string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private string _source = string.Empty;
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }
    }

    /// <summary>
    /// Tumblr link item data model.
    /// </summary>
    public class TumblrLinkDataItem : TumblrDataCommon
    {
        public static string STR_TITLE = "title";
        public static string STR_URL = "url";
        public static string STR_DESC = "description";

        public TumblrLinkDataItem()
            : base()
        {
        }

        public TumblrLinkDataItem(String jsonstring)
            : base(jsonstring)
        {
            JsonObject root = JsonObject.Parse(jsonstring);
            this._url = root.GetNamedString(STR_URL);
            this._title = root.GetNamedString(STR_TITLE);
            this._desc = root.GetNamedString(STR_DESC);
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _url = string.Empty;
        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }

        private string _desc = string.Empty;
        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }
    }

    /// <summary>
    /// Tumblr chat item data model.
    /// </summary>
    public class TumblrChatDataItem : TumblrDataCommon
    {
        public static string STR_TITLE = "title";
        public static string STR_BODY = "body";
        public static string STR_DIALOGUE = "dialogue";
        public static string STR_NAME = "name";
        public static string STR_LABEL = "label";
        public static string STR_PHRASE = "phrase";

        public TumblrChatDataItem()
            : base()
        {
        }

        public TumblrChatDataItem(String jsonstring)
            : base(jsonstring)
        {
            JsonObject root = JsonObject.Parse(jsonstring);
            this._body = root.GetNamedString(STR_BODY);
            this._title = root.GetNamedString(STR_TITLE);
            setDialogueFromArray(root.GetNamedArray(STR_DIALOGUE));
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _body = string.Empty;
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        private Collection<ChatItem> _dialogue = null;
        private Collection<ChatItem> Dialogue
        {
            get { return _dialogue; }
            set { _dialogue = value; }
        }

        private void setDialogueFromArray(JsonArray arr)
        {
            this._dialogue = new Collection<ChatItem>();
            foreach (JsonObject item in arr)
            {
                ChatItem c = new ChatItem();
                c.Name = item.GetNamedString(STR_NAME);
                c.Label = item.GetNamedString(STR_LABEL);
                c.Phrase = item.GetNamedString(STR_PHRASE);
                this._dialogue.Add(c);
            }
        }

        private class ChatItem
        {
            private string _name = string.Empty;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private string _label = string.Empty;
            public string Label
            {
                get { return _label; }
                set { _label = value; }
            }

            private string _phrase = string.Empty;
            public string Phrase
            {
                get { return _phrase; }
                set { _phrase = value; }
            }
        }
    }

    /// <summary>
    /// Tumblr audio item data model.
    /// </summary>
    public class TumblrAudioDataItem : TumblrDataCommon
    {
        public static string STR_CAPTION = "caption";
        public static string STR_PLAYER = "player";
        public static string STR_PLAYS = "plays";
        public static string STR_ALBUM_ART = "album_art";
        public static string STR_ARTIST = "artist";
        public static string STR_ALBUM = "album";
        public static string STR_TRACK_NAME = "track_name";
        public static string STR_TRACK_NUMBER = "track_number";
        public static string STR_YEAR = "year";

        public TumblrAudioDataItem()
            : base()
        {
        }

        public TumblrAudioDataItem(String jsonstring)
            : base(jsonstring)
        {
            JsonObject root = JsonObject.Parse(jsonstring);
            this._player = root.GetNamedString(STR_PLAYER);
            this._caption = root.GetNamedString(STR_CAPTION);
            this._plays = (int)root.GetNamedNumber(STR_PLAYS);
            this._albumart = root.GetNamedString(STR_ALBUM_ART);
            this._artist = root.GetNamedString(STR_ARTIST);
            this._album = root.GetNamedString(STR_ALBUM);
            this._trackname = root.GetNamedString(STR_TRACK_NAME);
            this._tracknum = (int)root.GetNamedNumber(STR_TRACK_NUMBER);
            this._year = (int)root.GetNamedNumber(STR_YEAR);
        }

        private string _caption = string.Empty;
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        private string _player = string.Empty;
        public string Player
        {
            get { return _player; }
            set { _player = value; }
        }

        private int _plays = 0;
        public int Plays
        {
            get { return _plays; }
            set { _plays = value; }
        }

        private string _albumart = string.Empty;
        public string AlbumArt
        {
            get { return _albumart; }
            set { _albumart = value; }
        }

        private string _artist = string.Empty;
        public string Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }

        private string _album = string.Empty;
        public string Album
        {
            get { return _album; }
            set { _album = value; }
        }

        private string _trackname = string.Empty;
        public string TrackName
        {
            get { return _trackname; }
            set { _trackname = value; }
        }

        private int _tracknum = 0;
        public int TrackNum
        {
            get { return _tracknum; }
            set { _tracknum = value; }
        }

        private int _year = 0;
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }
    }

    /// <summary>
    /// Tumblr video item data model.
    /// </summary>
    public class TumblrVideoDataItem : TumblrDataCommon
    {
        public static string STR_CAPTION = "caption";
        public static string STR_PLAYER = "player";
        public static string STR_WIDTH = "width";
        public static string STR_EMBED = "embed_code";

        public TumblrVideoDataItem()
            : base()
        {
        }

        public TumblrVideoDataItem(String jsonstring)
            : base(jsonstring)
        {
            JsonObject root = JsonObject.Parse(jsonstring);
            this._caption = root.GetNamedString(STR_CAPTION);
            setPlayersFromArr(root.GetNamedArray(STR_PLAYER));
        }

        private string _caption = string.Empty;
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        private Collection<VideoItem> _player = null;
        private Collection<VideoItem> Player
        {
            get { return _player; }
            set { _player = value; }
        }

        private void setPlayersFromArr(JsonArray arr)
        {
            this._player = new Collection<VideoItem>();
            foreach (JsonObject item in arr)
            {
                VideoItem v = new VideoItem();
                v.Width = (int)item.GetNamedNumber(STR_WIDTH);
                v.Embed = item.GetNamedString(STR_EMBED);
                this._player.Add(v);
            }
        }

        private class VideoItem
        {
            private int _width = 0;
            public int Width
            {
                get { return _width; }
                set { _width = value; }
            }

            private string _embed = string.Empty;
            public string Embed
            {
                get { return _embed; }
                set { _embed = value; }
            }
        }
    }

    /// <summary>
    /// Tumblr answer item data model.
    /// </summary>
    public class TumblrAnswerDataItem : TumblrDataCommon
    {
        public static string STR_ASK_NAME = "asking_name";
        public static string STR_ASK_URL = "asking_url";
        public static string STR_QUESTION = "question";
        public static string STR_ANSWER = "answer";

        public TumblrAnswerDataItem()
            : base()
        {
        }

        public TumblrAnswerDataItem(String jsonstring)
            : base(jsonstring)
        {
            JsonObject root = JsonObject.Parse(jsonstring);
            this._askurl = root.GetNamedString(STR_ASK_URL);
            this._askname = root.GetNamedString(STR_ASK_NAME);
            this._question = root.GetNamedString(STR_QUESTION);
            this._answer = root.GetNamedString(STR_ANSWER);
        }

        private string _askname = string.Empty;
        public string AskName
        {
            get { return _askname; }
            set { _askname = value; }
        }

        private string _askurl = string.Empty;
        public string AskUrl
        {
            get { return _askurl; }
            set { _askurl = value; }
        }

        private string _question = string.Empty;
        public string Question
        {
            get { return _question; }
            set { _question = value; }
        }

        private string _answer = string.Empty;
        public string Answer
        {
          get { return _answer; }
          set { _answer = value; }
        }

    }

    /// <summary>
    /// Tumblr group data model.
    /// </summary>
    public class TumblrDataGroup : TumblrDataCommon
    {
        public static string STR_TITLE = "title";
        public static string STR_POSTS = "posts";
        public static string STR_NAME = "name";
        public static string STR_UPDATED = "updated";
        public static string STR_DESC = "description";
        public static string STR_ASK = "ask";
        public static string STR_ANON = "ask_anon";
        public static string STR_LIKES = "likes";
        public static string STR_URL = "url";

        public TumblrDataGroup()
            : base()
        {
        }

        public TumblrDataGroup(String jsonvalues)
            : base(jsonvalues)
        {
            JsonObject root = JsonObject.Parse(jsonvalues);
            this._title = root.GetNamedString(STR_TITLE);
            this._posts = (int) root.GetNamedNumber(STR_POSTS);
            this._name = root.GetNamedString(STR_NAME);
            this._updated = (int) root.GetNamedNumber(STR_UPDATED);
            this._description = root.GetNamedString(STR_DESC);
            this._ask = root.GetNamedBoolean(STR_ASK);
            this._askanon = root.GetNamedBoolean(STR_ANON);
            this._likes = (int) root.GetNamedNumber(STR_LIKES);
            this._url = root.GetNamedString(STR_URL);
        }

        private ObservableCollection<TumblrDataCommon> _items = new ObservableCollection<TumblrDataCommon>();
        public ObservableCollection<TumblrDataCommon> Items
        {
            get { return this._items; }
        }

        public IEnumerable<TumblrDataCommon> TopItems
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            //
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed
            get { return this._items.Take(12); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private long _posts = 0;
        public long Posts
        {
            get { return _posts; }
            set { _posts = value; }
        }

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private long _updated = 0;
        public long Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }

        private bool _ask = false;
        public bool Ask
        {
            get { return _ask; }
            set { _ask = value; }
        }

        private bool _askanon = false;
        public bool Askanon
        {
            get { return _askanon; }
            set { _askanon = value; }
        }

        private long _likes = 0;
        public long Likes
        {
            get { return _likes; }
            set { _likes = value; }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private string _url = string.Empty;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }


        private ImageSource _groupImage;
        private string _groupImagePath;

        public ImageSource GroupImage
        {
            get
            {
                if (this._groupImage == null && this._groupImagePath != null)
                {
                    this._groupImage = new BitmapImage(new Uri(TumblrDataCommon._baseUri, this._groupImagePath));
                }
                return this._groupImage;
            }
            set
            {
                this._groupImagePath = null;
                this.SetProperty(ref this._groupImage, value);
            }
        }

        public int PostsCount
        {
            get
            {
                return this.Items.Count;
            }
        }

        public void SetGroupImage(String path)
        {
            this._groupImage = null;
            this._groupImagePath = path;
            this.OnPropertyChanged("GroupImage");
        }
    }

    /// <summary>
    /// Creates a collection of groups and items.
    /// </summary>
    public sealed class TumblrDataSource
    {
        //public event EventHandler RecipesLoaded;

        private static TumblrDataSource _tumblrDataSource = new TumblrDataSource();

        private ObservableCollection<TumblrDataGroup> _allGroups = new ObservableCollection<TumblrDataGroup>();
        public ObservableCollection<TumblrDataGroup> AllGroups
        {
            get { return this._allGroups; }
        }

        public static IEnumerable<TumblrDataGroup> GetGroups(String uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return _tumblrDataSource.AllGroups;
        }

        public static TumblrDataGroup GetGroup(String id)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _tumblrDataSource.AllGroups.Where((group) => group.Id.Equals(id));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static TumblrDataCommon GetItem(String id)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _tumblrDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.Id.Equals(id));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task LoadRemoteDataAsync()
        {
            // Retrieve dashboard data from tumblr
            var client = new HttpClient();
            client.MaxResponseContentBufferSize = 1024 * 1024; // Read up to 1 MB of data
            var response = await client.GetAsync(new Uri("http://api.tumblr.com/v2/blog/optimuscline.com/posts/?api_key=rJfXOBD2kHRdY9uj45CAztR1R9QbGSSCBh1z3QTLd0kJI2y5a0"));
            var result = await response.Content.ReadAsStringAsync();

            // Parse the JSON recipe data
            JsonObject resp = JsonObject.Parse(result);
            if (resp.GetNamedObject("meta").GetNamedString("msg").Equals("OK"))
            {
                JsonArray posts = resp.GetNamedObject("response").GetNamedArray("posts");
                string name = resp.GetNamedObject("response").GetNamedObject("blog").GetNamedString("url");
                CreateTumblrGroup(resp.GetNamedObject("response").GetNamedObject("blog"));
                // Convert the JSON objects into TumblrDataItems and TumblrDataGroups
                AddPostsToGroup(posts, name);
            }
        }

        public static async Task LoadLocalDataAsync()
        {
            // Retrieve recipe data from Recipes.txt
            var file = await Package.Current.InstalledLocation.GetFileAsync("Data\\Posts.txt");
            var result = await FileIO.ReadTextAsync(file);

            JsonObject resp = JsonObject.Parse(result);
            if (resp.GetNamedObject("meta").GetNamedString("msg").Equals("OK"))
            {
                JsonArray posts = resp.GetNamedObject("response").GetNamedArray("posts");
                string name = resp.GetNamedObject("response").GetNamedObject("blog").GetNamedString("url");
                CreateTumblrGroup(resp.GetNamedObject("response").GetNamedObject("blog"));
                // Convert the JSON objects into TumblrDataItems and TumblrDataGroups
                AddPostsToGroup(posts, name);
            }
        }

        private static void AddPostsToGroup(JsonArray array, String id)
        {
            foreach (JsonObject item in array)
            {
                TumblrDataCommon post = null;
                string type = item.GetNamedString(TumblrDataCommon.STR_TYPE);

                switch (type)
                {
                    // text, quote, link, answer, video, audio, photo, chat
                    case "text":
                        post = new TumblrTextDataItem(item.ToString());
                        break;
                    case "quote":
                        post = new TumblrQuoteDataItem(item.ToString());
                        break;
                    case "link":
                        post = new TumblrLinkDataItem(item.ToString());
                        break;
                    case "answer":
                        post = new TumblrAnswerDataItem(item.ToString());
                        break;
                    case "video":
                        post = new TumblrVideoDataItem(item.ToString());
                        break;
                    case "audio":
                        post = new TumblrAudioDataItem(item.ToString());
                        break;
                    case "photo":
                        post = new TumblrPhotoDataItem(item.ToString());
                        break;
                    case "chat":
                        post = new TumblrChatDataItem(item.ToString());
                        break;
                }

                if (post != null)
                    GetGroup(id).Items.Add(post);
            }
        }

        private static TumblrDataGroup CreateTumblrGroup(JsonObject obj)
        {
            TumblrDataGroup group = new TumblrDataGroup(obj.ToString());
            string hostname = group.Url;
            hostname = hostname.Substring(9);
            hostname = hostname.Substring(0, hostname.Length - 2);
            group.SetGroupImage("http://api.tumblr.com/v2/blog/" + hostname + "/avatar/512");

            _tumblrDataSource.AllGroups.Add(group);
            return group;
        }
    }
}
