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
    /// Base class for <see cref="TumblrDataItem"/> and <see cref="TumblrDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class TumblrDataCommon : tumbl8r.Common.BindableBase
    {

        internal static Uri _baseUri = new Uri("ms-appx:///");

        public TumblrDataCommon(string blogName, long id, string postURL, string type, long timestamp, string date, string format, string reblogKey,
                                string tags, bool bookmarkLet, bool mobile, string sourceURL, string sourceTitle, bool liked, 
                                string state, long totalPosts)
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
            // TODO something with the tags here
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

    }

    /// <summary>
    /// Recipe item data model.
    /// </summary>
    public class RecipeDataItem : TumblrDataCommon
    {
        public RecipeDataItem()
            : base(String.Empty, String.Empty, String.Empty, String.Empty)
        {
        }

        public RecipeDataItem(String uniqueId, String title, String shortTitle, String imagePath, int preptime, String directions, ObservableCollection<string> ingredients, RecipeDataGroup group)
            : base(uniqueId, title, shortTitle, imagePath)
        {
            this._preptime = preptime;
            this._directions = directions;
            this._ingredients = ingredients;
            this._group = group;
        }

        private int _preptime = 0;
        public int PrepTime
        {
            get { return this._preptime; }
            set { this.SetProperty(ref this._preptime, value); }
        }

        private string _directions = string.Empty;
        public string Directions
        {
            get { return this._directions; }
            set { this.SetProperty(ref this._directions, value); }
        }

        private ObservableCollection<string> _ingredients;
        public ObservableCollection<string> Ingredients
        {
            get { return this._ingredients; }
            set { this.SetProperty(ref this._ingredients, value); }
        }

        private RecipeDataGroup _group;
        public RecipeDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }

        private ImageSource _tileImage;
        private string _tileImagePath;

        public Uri TileImagePath
        {
            get
            {
                return new Uri(TumblrDataCommon._baseUri, this._tileImagePath);
            }
        }

        public ImageSource TileImage
        {
            get
            {
                if (this._tileImage == null && this._tileImagePath != null)
                {
                    this._tileImage = new BitmapImage(new Uri(TumblrDataCommon._baseUri, this._tileImagePath));
                }
                return this._tileImage;
            }
            set
            {
                this._tileImagePath = null;
                this.SetProperty(ref this._tileImage, value);
            }
        }

        public void SetTileImage(String path)
        {
            this._tileImage = null;
            this._tileImagePath = path;
            this.OnPropertyChanged("TileImage");
        }
    }

    /// <summary>
    /// Recipe group data model.
    /// </summary>
    public class RecipeDataGroup : TumblrDataCommon
    {
        public RecipeDataGroup()
            : base(String.Empty, String.Empty, String.Empty, String.Empty)
        {
        }

        public RecipeDataGroup(String uniqueId, String title, String shortTitle, String imagePath, String description)
            : base(uniqueId, title, shortTitle, imagePath)
        {
        }

        private ObservableCollection<RecipeDataItem> _items = new ObservableCollection<RecipeDataItem>();
        public ObservableCollection<RecipeDataItem> Items
        {
            get { return this._items; }
        }

        public IEnumerable<RecipeDataItem> TopItems
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

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
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

        public int RecipesCount
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
    public sealed class RecipeDataSource
    {
        //public event EventHandler RecipesLoaded;

        private static RecipeDataSource _recipeDataSource = new RecipeDataSource();

        private ObservableCollection<RecipeDataGroup> _allGroups = new ObservableCollection<RecipeDataGroup>();
        public ObservableCollection<RecipeDataGroup> AllGroups
        {
            get { return this._allGroups; }
        }

        public static IEnumerable<RecipeDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return _recipeDataSource.AllGroups;
        }

        public static RecipeDataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _recipeDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static RecipeDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _recipeDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task LoadRemoteDataAsync()
        {
            // Retrieve recipe data from Azure
            var client = new HttpClient();
            client.MaxResponseContentBufferSize = 1024 * 1024; // Read up to 1 MB of data
            var response = await client.GetAsync(new Uri("http://contosorecipes8.blob.core.windows.net/AzureRecipesRP"));
            var result = await response.Content.ReadAsStringAsync();

            // Parse the JSON recipe data
            var recipes = JsonArray.Parse(result);

            // Convert the JSON objects into RecipeDataItems and RecipeDataGroups
            CreateRecipesAndRecipeGroups(recipes);
        }

        public static async Task LoadLocalDataAsync()
        {
            // Retrieve recipe data from Recipes.txt
            var file = await Package.Current.InstalledLocation.GetFileAsync("Data\\Recipes.txt");
            var result = await FileIO.ReadTextAsync(file);

            // Parse the JSON recipe data
            var recipes = JsonArray.Parse(result);

            // Convert the JSON objects into RecipeDataItems and RecipeDataGroups
            CreateRecipesAndRecipeGroups(recipes);
        }

        private static void CreateRecipesAndRecipeGroups(JsonArray array)
        {
            foreach (var item in array)
            {
                var obj = item.GetObject();
                RecipeDataItem recipe = new RecipeDataItem();
                RecipeDataGroup group = null;

                foreach (var key in obj.Keys)
                {
                    IJsonValue val;
                    if (!obj.TryGetValue(key, out val))
                        continue;

                    switch (key)
                    {
                        case "key":
                            recipe.UniqueId = val.GetNumber().ToString();
                            break;
                        case "title":
                            recipe.Title = val.GetString();
                            break;
                        case "shortTitle":
                            recipe.ShortTitle = val.GetString();
                            break;
                        case "preptime":
                            recipe.PrepTime = (int)val.GetNumber();
                            break;
                        case "directions":
                            recipe.Directions = val.GetString();
                            break;
                        case "ingredients":
                            var ingredients = val.GetArray();
                            var list = (from i in ingredients select i.GetString()).ToList();
                            recipe.Ingredients = new ObservableCollection<string>(list);
                            break;
                        case "backgroundImage":
                            recipe.SetImage(val.GetString());
                            break;
                        case "tileImage":
                            recipe.SetTileImage(val.GetString());
                            break;
                        case "group":
                            var recipeGroup = val.GetObject();

                            IJsonValue groupKey;
                            if (!recipeGroup.TryGetValue("key", out groupKey))
                                continue;

                            group = _recipeDataSource.AllGroups.FirstOrDefault(c => c.UniqueId.Equals(groupKey.GetString()));

                            if (group == null)
                                group = CreateRecipeGroup(recipeGroup);

                            recipe.Group = group;
                            break;
                    }
                }

                if (group != null)
                    group.Items.Add(recipe);
            }
        }

        private static RecipeDataGroup CreateRecipeGroup(JsonObject obj)
        {
            RecipeDataGroup group = new RecipeDataGroup();

            foreach (var key in obj.Keys)
            {
                IJsonValue val;
                if (!obj.TryGetValue(key, out val))
                    continue;

                switch (key)
                {
                    case "key":
                        group.UniqueId = val.GetString();
                        break;
                    case "title":
                        group.Title = val.GetString();
                        break;
                    case "shortTitle":
                        group.ShortTitle = val.GetString();
                        break;
                    case "description":
                        group.Description = val.GetString();
                        break;
                    case "backgroundImage":
                        group.SetImage(val.GetString());
                        break;
                    case "groupImage":
                        group.SetGroupImage(val.GetString());
                        break;
                }
            }

            _recipeDataSource.AllGroups.Add(group);
            return group;
        }
    }
}
