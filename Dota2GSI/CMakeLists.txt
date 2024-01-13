# Setup project.
SET(PRODUCT Dota2GSI)
SET(PRODUCT_NAME "${PRODUCT}")
SET(PRODUCT_VERSION 2.0.0)
SET(PRODUCT_DESCRIPTION "A C# library to interface with the Game State Integration found in Dota 2.")
SET(PRODUCT_AUTHORS "Anton Pupkov")
SET(PRODUCT_COPYRIGHT "Copyright© 2024")

if(DEFINED BUILD_NUMBER)
    SET(PRODUCT_VERSION "${PRODUCT_VERSION}.${BUILD_NUMBER}")
endif()

PROJECT(${PRODUCT} VERSION ${PRODUCT_VERSION} LANGUAGES CSharp)

# Setup project to use C# utilities.
INCLUDE(CSharpUtilities)

SET(SOURCES
    GameState.cs
    GameStateListener.cs
    Nodes/Roshan.cs
    Nodes/HeroProvider/HeroDetails.cs
    Nodes/CouriersProvider/Courier.cs
    Nodes/MinimapProvider/MinimapElement.cs
    Nodes/Helpers/Vector2D.cs
    Nodes/Helpers/FullTeamDetails.cs
    Nodes/Helpers/FullPlayerDetails.cs
    Nodes/RoshanProvider/ItemsDrop.cs
    Nodes/League.cs
    Nodes/LeagueProvider/Stream.cs
    Nodes/Wearables.cs
    Nodes/WearablesProvider/WearableItem.cs
    Nodes/WearablesProvider/PlayerWearables.cs
    Nodes/NeutralItemsProvider/NeutralTierInfo.cs
    Nodes/NeutralItems.cs
    Nodes/NeutralItemsProvider/TeamNeutralItems.cs
    Nodes/NeutralItemsProvider/NeutralItem.cs
    Nodes/Minimap.cs
    Nodes/Hero.cs
    Nodes/LeagueProvider/SelectionPriority.cs
    Nodes/LeagueProvider/LeagueTeam.cs
    Nodes/Items.cs
    Nodes/ItemsProvider/Item.cs
    Nodes/ItemsProvider/ItemDetails.cs
    Nodes/Events.cs
    Nodes/EventsProvider/Event.cs
    Nodes/Draft.cs
    Nodes/DraftProvider/DraftDetails.cs
    Nodes/Couriers.cs
    Nodes/CouriersProvider/CourierItem.cs
    Nodes/Buildings.cs
    Nodes/BuildingsProvider/BuildingLayout.cs
    Nodes/BuildingsProvider/Building.cs
    Nodes/Abilities.cs
    Nodes/AbilitiesProvider/Ability.cs
    Nodes/AbilitiesProvider/AbilityDetails.cs
    Nodes/Player.cs
    Nodes/PlayerProvider/PlayerDetails.cs
    Nodes/Node.cs
    Nodes/Provider.cs
    Nodes/Map.cs
    Nodes/Auth.cs
)

SET(README "${CMAKE_SOURCE_DIR}/README.md")

# Add shared library project.
ADD_LIBRARY(${PROJECT_NAME} SHARED
    ${README}
    ${SOURCES}
)

# Define dependencies.
TARGET_LINK_LIBRARIES(${PROJECT_NAME} 
    PUBLIC CommonLib
)

SET(NET_REFERENCES
    )

SET(NUGET_PACKAGES
    "Microsoft.CSharp_4.7.0"
    "System.Data.DataSetExtensions_4.5.0"
    "Newtonsoft.Json_13.0.3"
    )

# Set .Net
SET_TARGET_PROPERTIES(${PROJECT_NAME} PROPERTIES
    DOTNET_SDK "Microsoft.NET.Sdk"
    DOTNET_TARGET_FRAMEWORK "net8.0"
    VS_DOTNET_REFERENCES "${NET_REFERENCES}"
    VS_GLOBAL_ROOTNAMESPACE ${PROJECT_NAME}
    VS_PACKAGE_REFERENCES "${NUGET_PACKAGES}"
)

# Set readme file
SET_SOURCE_FILES_PROPERTIES(${README} PROPERTIES
    VS_CSHARP_Pack "True"
    VS_CSHARP_PackagePath "\\")

# Set project configuration
SET_TARGET_PROPERTIES(${PROJECT_NAME} PROPERTIES
    VS_GLOBAL_GenerateAssemblyInfo "True"
    VS_GLOBAL_GenerateDocumentationFile "True"
    VS_GLOBAL_GeneratePackageOnBuild "True"
    VS_GLOBAL_TreatWarningsAsErrors "True"
    VS_GLOBAL_PackageProjectUrl ${REPOSITORY_URL}
    VS_GLOBAL_RepositoryUrl ${REPOSITORY_URL}
    VS_GLOBAL_PackageReadmeFile "README.md"
)

# Set Assembly information
SET_TARGET_PROPERTIES(${PROJECT_NAME} PROPERTIES
    VS_GLOBAL_Authors "${PRODUCT_AUTHORS}"
    VS_GLOBAL_Version "${PRODUCT_VERSION}"
    VS_GLOBAL_Description "${PRODUCT_DESCRIPTION}"
    VS_GLOBAL_Product "${PRODUCT_NAME}"
    VS_GLOBAL_Copyright "${PRODUCT_COPYRIGHT}"
)

# Export config.
EXPORT(TARGETS ${PROJECT_NAME} FILE ${PROJECT_NAME}Config.cmake)