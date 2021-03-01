public class LegoInfoMM
{
    /* 
      Below are defined sizes for lego pieces. 
      All measurements are in millimeters(mm).   
    */

    // conversion from ldu to mm
    public const double MM_TO_LDU_RATIO = 0.4;

    /**************************/
    /* 1x1 Lego Measurements */
    /**************************/
    // Standard Lego Measurements
    public const double LEGO_WIDTH = 8; // X or Z Size
    public const double LEGO_DEPTH = 8; // X or Z Size

    // Stud Measurements
    public const double STUD_HEIGHT = 1.6;
    public const double STUD_RADIUS = 2.4;
    public const double STUD_DIAMETER = 4.8;

    // distances between two adjacent studs
    public const double DISTANCE_BETWEEN_STUD_CENTERS = 8;
    public const double DISTANCE_BETWEEN_STUD_EDGES = 3.2;

    /**************************/
    /* 1x1 Brick Measurements */
    /**************************/
    public const double BRICK_HEIGHT_WITHOUT_STUD = 9.6; // Y Size

    /**************************/
    /* 1x1 Plate Measurements */
    /**************************/
    public const double PLATE_HEIGHT_WITHOUT_STUD = 3.2; // Y Size
}
