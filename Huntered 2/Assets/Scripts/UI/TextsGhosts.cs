using System.Collections.Generic;

public class TextsGhosts {

    //////////////////////////////
    // ARTICLES
    //////////////////////////////

    public static List<string[]> GhostArticles = new List<string[]>();

    public static string[] GhostArticlesEN = {
        "Lorem",
        "Lorem",
        "Lorem"
    };

    public static string[] GhostArticlesDE = {
        "Enorm",
        "Lorem",
        "Lorem"
    };

    public static string[] GhostArticlesSW = {
        "Enorm",
        "Scho",
        "Recht",
        "Arg",
        "Brutal",
        "Sau",
        "Saumäßig",
        "Ein",
        "Dermaßen",
        "Super",
        "Extrem",
        "Sehr",
        "Auzoga",
        "Elendig"
    };


    //////////////////////////////
    // ADJECTIVES
    //////////////////////////////

    public static List<string[]> GhostAdjectives = new List<string[]>();

    public static string[] GhostAdjectivesEN = {
        "Lorem",
        "Lorem",
        "Lorem"
    };

    public static string[] GhostAdjectivesDE = {
        "Lorem",
        "Lorem",
        "Lorem"
    };

    public static string[] GhostAdjectivesSW = {
        "Bleeder",
        "Cleverer",
        "Trottliger",
        "Pimmliger",
        "Schneller",
        "Gscheiter",
        "Brutaler",
        "Auzogener",
        "Näckiger",
        "Seckliger",
        "Hobliger",
        "Dümmlicher",
        "Auneitiger",
        "Augriabiger",
    };


    //////////////////////////////
    // NAMES
    //////////////////////////////

    public static List<string[]> GhostNames = new List<string[]>();

    public static string[] GhostNamesDE = {
        "Lorem",
        "Lorem",
        "Lorem"
    };

    public static string[] GhostNamesEN = {
        "Lorem",
        "Lorem",
        "Lorem"
    };

    public static string[] GhostNamesSW = {
        "Manu",
        "Benny",
        "Schwede",
        "Alex",
        "Marcel",
        "Thomas",
        "Jockel",
        "Depp",
        "Aff",
    };


    //////////////////////////////
    //////////////////////////////
    //////////////////////////////


    public static void InitializeTexts() {
        GhostArticles.Add(GhostArticlesEN);
        GhostArticles.Add(GhostArticlesDE);
        GhostArticles.Add(GhostArticlesSW);

        GhostAdjectives.Add(GhostAdjectivesEN);
        GhostAdjectives.Add(GhostAdjectivesDE);
        GhostAdjectives.Add(GhostAdjectivesSW);

        GhostNames.Add(GhostNamesEN);
        GhostNames.Add(GhostNamesDE);
        GhostNames.Add(GhostNamesSW);
    }
}
