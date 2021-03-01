public class LegoInfoLDU
{
    /* 
      Below are defined sizes for lego pieces. 
      All measurements are in LDU.
      LDU is a simple unit that can simplify the measurements of various lego measurements.
      1 LDU = 0.4 mm;   
    */

    // conversion from ldu to mm
    public const double LDU_TO_MM_RATIO = 2.5;

    /**************************/
    /* 1x1 Lego Measurements  */
    /**************************/
    // Standard Lego Measurements
    public const double LEGO_WIDTH = 20; // X or Z Size
    public const double LEGO_DEPTH = 20; // X or Z Size

    // Stud Measurements
    public const double STUD_HEIGHT = 4;
    public const double STUD_RADIUS = 6;
    public const double STUD_DIAMETER = 12;

    // distances between two adjacent studs
    public const double DISTANCE_BETWEEN_STUD_CENTERS = 40;
    public const double DISTANCE_BETWEEN_STUD_EDGES = 8;

    /**************************/
    /* 1x1 Brick Measurements */
    /**************************/
    public const double BRICK_HEIGHT = 24; // Y Size

    /**************************/
    /* 1x1 Plate Measurements */
    /**************************/
    public const double PLATE_HEIGHT = 8; // Y Size
}
